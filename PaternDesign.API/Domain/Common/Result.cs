namespace PaternDesign.API.Domain.Common
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<string> ErrorMessages { get; set; }

        // Constructor para resultado exitoso
        public Result(T data)
        {
            Success = true;
            Data = data;
            ErrorMessages = new List<string>();
        }

        // Constructor para resultado fallido con lista de mensajes
        public Result(List<string> errorMessages)
        {
            Success = false;
            Data = default;
            ErrorMessages = errorMessages ?? new List<string>();
        }

        // Helper para crear un resultado exitoso
        public static Result<T> SuccessResult(T data)
        {
            return new Result<T>(data);
        }

        // Helper para crear un resultado fallido con lista de errores
        public static Result<T> FailureResult(List<string> errorMessages)
        {
            return new Result<T>(errorMessages);
        }

        // Helper para crear un resultado fallido con un solo mensaje de error
        public static Result<T> FailureResult(string errorMessage)
        {
            return new Result<T>(new List<string> { errorMessage });
        }
    }
}
