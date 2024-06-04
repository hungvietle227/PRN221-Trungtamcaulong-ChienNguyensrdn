using BadmintonCenter.Common.Constant.Message;
using BadmintonCenter.Common.Enum.Status;
using System.Web;

namespace BadmintonCenter.Presentation.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
                if(context.Response.StatusCode == 404)
                {
                    HandleNotFound(context);
                }
            }
            catch (Exception ex)
            {
                HandleError(context, ex);
            }
        }

        private static void HandleError(HttpContext context, Exception ex)
        {
            List<string> errors = new() { ex.Message };

            var errorMsg = string.Join(",", errors.Select(HttpUtility.UrlEncode));

            context.Response.Redirect($"/error?errMessage={errorMsg}&statusCode={(int)StatusCode.InternalServer}");
        }

        private static void HandleNotFound(HttpContext context)
        {
            context.Response.Redirect($"/error?errMessage={ErrorMessage.NotFound}&statusCode={(int)StatusCode.NotFound}");
        }
    }
}
