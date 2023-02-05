
namespace ObakiSite.Application.Shared
{
    public class Error : IEquatable<Error>
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error EmptyValue = new("Error.EmptyValue", "Result is empty.");
        public static Error HttpError(string code) => new($"HttpError - {code}", "Result encountered a Http error");
        public Error(string code, string messaage)
        {
            Code = code;
            Message = messaage;
        }

        public string Code { get; }

        public string Message { get; }

        public bool Equals(Error? other)
        {
            if (other is null)
                return false;

            if (other.GetType() != GetType())
                return false;

            return true;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            if (obj is not Error entity)
                return false;

            return true;
        }
        public static bool operator ==(Error? first, Error? second)
             => first is not null && second is not null && first.Equals(second);

        public static bool operator !=(Error? first, Error? second)
             => !(first == second);

        public static implicit operator string(Error error) 
             => error.Code;


        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}
