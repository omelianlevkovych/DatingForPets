namespace API.Errors
{
    /// <summary>
    /// The API exception class.
    /// </summary>
    public class ApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException" /> class.
        /// </summary>
        /// <param name="statusCode">The exception status code.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="details">Exception details.</param>
        public ApiException(int statusCode, string message = null, string details = null)
        {
        StatusCode = statusCode;
        Message = message;
        Details = details;
        }

        /// <summary>
        /// Gets or sets the exeption status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the exception message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets exception details.
        /// </summary>
        public string Details { get; set; }
        }
}