using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.ComponentModel.DataAnnotations;

using Backend.Application.Wrappers;
using System.Collections.Generic;

namespace Backend.Application.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
         
        public virtual void OnActionExecuting(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                Response<bool> apiResponse = new Response<bool>
                {
                    //Success = false,
                    //Message = "One or more validation errors occurred.",
                    //Errors = ""
                };

                /*foreach (var modelState in context.ModelState)
                {
                    apiResponse.Errors.Add(modelState.Key, modelState.Value.Errors.Select(a => a.ErrorMessage).ToList());
                }*/

                //context.Result = new BadRequestObjectResult(context.ModelState);
                //context.Result = new BadRequestObjectResult(apiResponse);
            }
            base.OnActionExecuted(context);
        }
        /*public override void OnActionExecuted(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                Response<bool> apiResponse = new Response<bool>
                {
                    Success = false,
                    Message = "One or more validation errors occurred.",
                    //Errors = ""
                };

                /*foreach (var modelState in context.ModelState)
                {
                    apiResponse.Errors.Add(modelState.Key, modelState.Value.Errors.Select(a => a.ErrorMessage).ToList());
                }

                //context.Result = new BadRequestObjectResult(context.ModelState);
                //context.Result = new BadRequestObjectResult(apiResponse);
            }
            base.OnActionExecuted(context);
        }*/
    }
}
