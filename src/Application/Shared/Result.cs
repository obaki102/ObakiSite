
namespace ObakiSite.Application.Shared
{
    public class Result
    {
        public bool IsSuccess { get; }
        public Error Error { get; }
        public bool IsFailure => !IsSuccess;

        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
                throw new InvalidOperationException();

            if (!isSuccess && error == Error.None)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Fail(Error error)
        {
            return new Result(false, error);
        }

        public static Result<T> Fail<T>(Error error)
        {
            return new Result<T>(default(T), false, error);
        }

        public static Result Success()
        {
            return new Result(true, Error.None);
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value, true, Error.None);
        }
      
    }


}
