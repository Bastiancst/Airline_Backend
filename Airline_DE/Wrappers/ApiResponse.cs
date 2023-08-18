namespace Airline_DE.Wrappers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Result { get; set; }

        public ApiResponse()
        {

        }

        public ApiResponse(T data, string message = null)
        {
            Success = true;
            Message = message;
            Result = data;
        }

        public ApiResponse(string message)
        {
            Success = false;
            Message = message;
        }
    }

}
