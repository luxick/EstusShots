using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EstusShots.Server.Filters
{
    /// <summary>
    /// Adds 2 seconds of wait time to each request 
    /// </summary>
    public class DebugDelayFilter : IActionFilter

    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Task.Delay(2000).Wait();
        }
    }
}