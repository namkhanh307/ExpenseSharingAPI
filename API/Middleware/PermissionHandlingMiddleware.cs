using Core.Infrastructure;
using Repositories.IRepositories;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class PermissionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PermissionHandlingMiddleware> _logger;
        private readonly IEnumerable<string> _excludedUris;
        private readonly IHttpContextAccessor _contextAccessor;


        public PermissionHandlingMiddleware(RequestDelegate next, ILogger<PermissionHandlingMiddleware> logger, IHttpContextAccessor contextAccessor)
        {
            _next = next;
            _logger = logger;
            _excludedUris = new List<string>()
            {
                "/api/Auth/login",
                "/api/auth/signup",
            };
            _contextAccessor = contextAccessor;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork)
        {
            if (HasPermission(context))
            {
                await _next(context);
            }
            else
            {
                var code = HttpStatusCode.Forbidden;
                var result = JsonSerializer.Serialize(new { error = "You don't have permission to access this feature" });
                context.Response.ContentType = "application/json";
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.StatusCode = (int)code;
                await context.Response.WriteAsync(result);
            }
        }

        private bool HasPermission(HttpContext context)
        {
            string requestUri = context.Request.Path.Value; ///api/dashboard
            if (_excludedUris.Contains(requestUri) || !requestUri.StartsWith("/api/")) return true;
            string idUser = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            try
            {
                {
                    if(idUser != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking permissions");
                return false;
            }
        }
    }
}
