using FluentValidation;
using MediatR;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Behaviours.Validation
{

    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
                                                            where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }
            var context = new ValidationContext<TRequest>(request);

            //var errorsDictionary = _validators
            //    .Select(x => x.Validate(context))
            //    .SelectMany(x => x.Errors)
            //    .Where(x => x != null)
            //    .GroupBy(
            //        x => x.PropertyName,
            //        x => x.ErrorMessage,
            //        (propertyName, errorMessages) => new
            //        {
            //            Key = propertyName,
            //            Values = errorMessages.Distinct().ToArray()
            //        })
            //    .ToDictionary(x => x.Key, x => x.Values);

            var errorLists = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null);

            if (errorLists.Any())
            {
                //TODO: Throw error to middleware to avoid reflection.
                //Check if response is an application response type
                if (typeof(TResponse).Name.Contains(typeof(ApplicationResponse).Name))
                {
                    //Build the parameter to be pass
                    var parameterType = new Type[] { typeof(List<string>) };
                    //Find the method "fail" in Application Response object
                    var failMethodInApplicationResponse = typeof(TResponse).GetMethod("Fail", parameterType);
                    //Create the Application Response object
                    var responseObject = (TResponse?)Activator.CreateInstance(typeof(TResponse));
                    //Pass the list of errors as a parameter
                    object[] parameters = new object[] { errorLists.Select(e => e.ErrorMessage).Distinct().ToList() };
                    return (TResponse?)failMethodInApplicationResponse.Invoke(responseObject, parameters);
                }
                // throw new ValidationException(errorLists);
            }

            return await next();
        }
    }

}
