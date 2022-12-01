
namespace ObakiSite.Shared.Models.Response
{
    public class ApplicationResponse : IApplicationResponse
    {
        public List<string> Messages { get; set; } = new List<string>();
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Returns a succesful synchronous response but with no messages 
        /// </summary>
        /// <returns></returns>
        public static ApplicationResponse Success()
        {
            return new ApplicationResponse { IsSuccess = true };
        }

        /// <summary>
        ///  Returns a succesful synchronous response  with one message 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApplicationResponse Success(string message)
        {
            return new ApplicationResponse { IsSuccess = true, Messages = new List<string> { message } };
        }

        /// <summary>
        /// Returns a succesful synchronous response  with more than one message 
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static ApplicationResponse Success(List<string> messages)
        {
            return new ApplicationResponse { IsSuccess = true, Messages = messages };
        }

        /// <summary>
        ///  Returns a failed synchronous response but with no message 
        /// </summary>
        /// <returns></returns>
        public static ApplicationResponse Fail()
        {
            return new ApplicationResponse { IsSuccess = false };
        }

        /// <summary>
        ///  Returns a failed synchronous response  with one message 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApplicationResponse Fail(string message)
        {
            return new ApplicationResponse { IsSuccess = false, Messages = new List<string> { message } };
        }

        /// <summary>
        /// Returns a failed synchronous response  with more than one message
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static ApplicationResponse Fail(List<string> messages)
        {
            return new ApplicationResponse { IsSuccess = false, Messages = messages };
        }

        /// <summary>
        /// Returns a succesful asynchronous response  but with no message 
        /// </summary>
        /// <returns></returns>
        public static Task<ApplicationResponse> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        /// <summary>
        ///  Returns a succesful asynchronous response  with one message 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Task<ApplicationResponse> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        /// <summary>
        /// Returns a succesful asynchronous response  with more than one message 
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static Task<ApplicationResponse> SuccessAsync(List<string> messages)
        {
            return Task.FromResult(Success(messages));
        }

        /// <summary>
        ///  Returns a failed asynchronous response but with no message 
        /// </summary>
        /// <returns></returns>
        public static Task<ApplicationResponse> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        /// <summary>
        ///  Returns a failed asynchronous response  with one message 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Task<ApplicationResponse> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        /// <summary>
        /// Returns a failed asynchronous response  with more than one message
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static Task<ApplicationResponse> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }
    }

    // TODO: Implement Async methods
    public class ApplicationResponse<T> : ApplicationResponse, IApplicationResponse<T>
    {
        public ApplicationResponse()
        {

        }
        public ApplicationResponse(T? data, List<string> message)
        {
            Data = data;
            Messages = message;
        }


        public T? Data { get; set; }

        /// <summary>
        /// Returns a succesful synchronous response but with no messages. No data is attached.
        /// </summary>
        /// <returns></returns>
        public static new ApplicationResponse<T> Success()
        {
            return new ApplicationResponse<T> { IsSuccess = true };
        }

        /// <summary>
        /// Returns a succesful synchronous response with one message. No data is attached.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static new ApplicationResponse<T> Success(string message)
        {
            return new ApplicationResponse<T> { IsSuccess = true, Messages = new List<string> { message } };
        }

        /// <summary>
        /// Returns a succesful synchronous response with more than one messages.  No data is attached.
        /// <param name="messages"></param>
        /// <returns></returns>
        public static new ApplicationResponse<T> Success(List<string> messages)
        {
            return new ApplicationResponse<T> { IsSuccess = true, Messages = messages };
        }

        /// <summary>
        /// Returns a succesful synchronous response but with no messages. Data is attached.
        /// </summary>
        /// <returns></returns>
        public static ApplicationResponse<T> Success(T data)
        {
            return new ApplicationResponse<T> { IsSuccess = true, Data = data };
        }


        /// <summary>
        /// Returns a succesful synchronous response with one message. Data is attached.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Success(string message, T data)
        {
            return new ApplicationResponse<T> { IsSuccess = true, Messages = new List<string> { message }, Data = data };
        }

        /// <summary>
        /// Returns a succesful synchronous response with more than one messages.  Data is attached.
        /// <param name="messages"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Success(List<string> messages, T data)
        {
            return new ApplicationResponse<T> { IsSuccess = true, Messages = messages, Data = data };
        }


        /// <summary>
        /// Returns a unsuccesful synchronous response but with no messages. No data is attached.
        /// </summary>
        /// <returns></returns>
        public static new ApplicationResponse<T> Fail()
        {
            return new ApplicationResponse<T> { IsSuccess = false };
        }

        /// <summary>
        /// Returns a unsuccesful synchronous response with one message. No data is attached.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static new ApplicationResponse<T> Fail(string message)
        {
            return new ApplicationResponse<T> { IsSuccess = false, Messages = new List<string> { message } };
        }

        /// <summary>
        /// Returns a unsuccesful synchronous response with more than one messages.  No data is attached.
        /// <param name="messages"></param>
        /// <returns></returns>
        public static new ApplicationResponse<T> Fail(List<string> messages)
        {
            return new ApplicationResponse<T> { IsSuccess = false, Messages = messages };
        }

        /// <summary>
        /// Returns a unsuccesful synchronous response but with no messages. Data is attached.
        /// </summary>
        /// <returns></returns>
        public static ApplicationResponse<T> Fail(T data)
        {
            return new ApplicationResponse<T> { IsSuccess = true, Data = data };
        }

        /// <summary>
        /// Returns a unsuccesful synchronous response with one message. Data is attached.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Fail(string message, T data)
        {
            return new ApplicationResponse<T> { IsSuccess = false, Messages = new List<string> { message }, Data = data };
        }

        /// <summary>
        /// Returns a unsuccesful synchronous response with more than one messages.  Data is attached.
        /// <param name="messages"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Fail(List<string> messages, T data)
        {
            return new ApplicationResponse<T> { IsSuccess = false, Messages = messages, Data = data };
        }

    }
}
