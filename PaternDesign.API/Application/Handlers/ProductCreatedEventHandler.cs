using MediatR;
using PaternDesign.API.Application.Abstractions;

namespace PaternDesign.API.Application.Handlers
{
    public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly ILogger<ProductCreatedEventHandler> _logger;

        public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Aquí puedes agregar cualquier acción que desees realizar
            _logger.LogInformation($"Producto creado: {notification.Product.ProductName} - {notification.Product.ProductPrice}");

            // Aquí podrías agregar más lógica: enviar correos, enviar notificaciones, etc.

            return Task.CompletedTask;
        }
    }
}
