using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookLibraryApi.ActionFilters;

public class ValidatorAsyncActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value is not null && x.Value.Errors.Count > 0)
                .ToDictionary(x => x.Key, y => y.Value!.Errors.Select(x => x.ErrorMessage).ToArray());

            var response = new ErrorResponse();

            foreach (var error in errors)
            {
                foreach (var subError in error.Value)
                {
                    var errorModel = new ErrorModel
                    {
                        FieldName = error.Key,
                        Message = subError
                    };

                    response.Errors.Add(errorModel);
                }
            }

            context.Result = new BadRequestObjectResult(response);
            return;

        }

        await next();
    }
}