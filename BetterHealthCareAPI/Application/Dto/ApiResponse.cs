namespace BetterHealthCareAPI.Application.Dto
{
    /// <summary>
    /// Standard API response wrapper for consistent response format
    /// </summary>
    /// <typeparam name="T">Type of data being returned</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indicates whether the request was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The actual data payload
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// User-friendly message describing the result
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Error details if the request failed
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// HTTP status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Creates a successful response
        /// </summary>
        public static ApiResponse<T> SuccessResponse(T data, string message = "Request successful", int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message,
                StatusCode = statusCode
            };
        }

        /// <summary>
        /// Creates a failed response
        /// </summary>
        public static ApiResponse<T> ErrorResponse(string error, int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Error = error,
                StatusCode = statusCode
            };
        }
    }

    /// <summary>
    /// Non-generic API response for operations without data return
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Indicates whether the request was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// User-friendly message describing the result
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Error details if the request failed
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// HTTP status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Creates a successful response
        /// </summary>
        public static ApiResponse SuccessResponse(string message = "Request successful", int statusCode = 200)
        {
            return new ApiResponse
            {
                Success = true,
                Message = message,
                StatusCode = statusCode
            };
        }

        /// <summary>
        /// Creates a failed response
        /// </summary>
        public static ApiResponse ErrorResponse(string error, int statusCode = 400)
        {
            return new ApiResponse
            {
                Success = false,
                Error = error,
                StatusCode = statusCode
            };
        }
    }
}
