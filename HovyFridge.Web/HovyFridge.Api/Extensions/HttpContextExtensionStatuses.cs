namespace HovyFridge.Api.Extensions
{
    public static class HttpContextExtensionStatuses
    {
        public static async Task AccessDenied(this HttpContext context, string message)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync(message);
        }
    }
}