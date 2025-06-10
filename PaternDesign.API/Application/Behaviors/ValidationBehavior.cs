using FluentValidation;
using MediatR;
using PaternDesign.API.Domain.Common;

namespace PaternDesign.API.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                // Ejecutamos todos los validadores y recogemos los resultados
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                // Recogemos todos los errores de validación
                var failures = validationResults.SelectMany(r => r.Errors)
                                                .Where(f => f != null)
                                                .ToList();

                if (failures.Any())
                {
                    // Aquí estamos devolviendo todos los errores concatenados
                    var errorMessages = failures.Select(f => f.ErrorMessage).ToList();

                    // Crear un objeto Result que contenga los errores como lista de strings
                    var failureResult = Result<string>.FailureResult(errorMessages);

                    // Retornamos el resultado con los errores
                    return (TResponse)Convert.ChangeType(failureResult, typeof(TResponse));
                }
            }

            // Si no hay errores, simplemente ejecutamos el handler
            return await next();
        }
    }
}
