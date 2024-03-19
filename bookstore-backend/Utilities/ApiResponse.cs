namespace bookstore_backend.Utilities
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }

        public ApiResponse(bool success, string? message = null, T data = default!)
        {
            Data = data;
            Success = success;
            Message = message;
        }
    }
}
