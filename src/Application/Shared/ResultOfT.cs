namespace ObakiSite.Application.Shared
{
    public class Result<TValue> : Result
    {   
        private readonly TValue? _value;
        public Result(TValue? value, bool isSuccess, string error) : base(isSuccess, error) => _value = value;

        public TValue? Value => _value!;

        public static implicit operator Result<TValue>(TValue value) => new(value,true,string.Empty);
       
    }
}
