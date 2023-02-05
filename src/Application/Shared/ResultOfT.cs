namespace ObakiSite.Application.Shared
{
    public class Result<TValue> : Result
    {   
        private readonly TValue? _value;
        protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error) => _value = value;

        public TValue? Value => IsSuccess
            ? _value! : throw new InvalidOperationException("Value of failure result is inaccessible");

        public static implicit operator Result<TValue>(TValue value) => new(value,true,Error.None);
       
    }
}
