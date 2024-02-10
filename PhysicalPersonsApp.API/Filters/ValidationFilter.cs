using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PhysicalPersons.API.Filters.FilterModels;

namespace PhysicalPersons.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var ErrorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage))
                    .ToArray();
                var ErrorResponse = new ErrorResponseModel();

                foreach (var Error in ErrorsInModelState)
                {
                    foreach (var SubError in Error.Value)
                    {
                        var ErrorModel = new ErrorModel()
                        {
                            FieldName = Error.Key,
                            Message = SubError
                        };
                        ErrorResponse.Errors.Add(ErrorModel);
                    }
                }
                context.Result = new BadRequestObjectResult(ErrorResponse);
                return;
            }
            await next();
        }
    }
}
