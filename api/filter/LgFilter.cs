using Core.Interface;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.filter
{
    public class LgFilter : Attribute, IActionFilter
    {
        private readonly IProductRepository _productRepository;
        public LgFilter()
        {
           
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
