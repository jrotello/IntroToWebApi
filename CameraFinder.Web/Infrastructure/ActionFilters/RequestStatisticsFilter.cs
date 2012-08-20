using System;
using System.Web.Http.Filters;
using CameraFinder.Web.Infrastructure.Extensions;

namespace CameraFinder.Web.Infrastructure.ActionFilters
{
    public class RequestStatisticsFilter: ActionFilterAttribute {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext) {
            var metadata = actionContext.Request.GetApiRequestMetaData();
            if (metadata != null) {
                metadata.ActionStart = DateTime.UtcNow;
                metadata.ControllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
                metadata.ActionName = actionContext.ActionDescriptor.ActionName;
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var metadata = actionExecutedContext.Request.GetApiRequestMetaData();
            if (metadata != null) {
                metadata.ActionEnd = DateTime.UtcNow;

                var elapsed = (metadata.ActionEnd - metadata.ActionStart) ?? TimeSpan.FromMilliseconds(0);
                metadata.ActionMilliseconds = elapsed.Milliseconds;
            }
        }
    }
}