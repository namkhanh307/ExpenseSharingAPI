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
        private readonly Dictionary<string, List<string>> _rolePermissions;
        private readonly IEnumerable<string> _excludedUris;


        public PermissionHandlingMiddleware(RequestDelegate next, ILogger<PermissionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _excludedUris = new List<string>()
            {
                "/api/auth/login",
                "/api/auth/register",
                "/api/roles",
                "/api/roles/getrolebynumber",
                "/api/modules"
            };
            _rolePermissions = new Dictionary<string, List<string>>()
            {
                //author bang role, roleClaim userClaim
                //{ "admin", new List<string> { "/api/categories", "/api/roles", "/api/modules", "/api/user", "/api/profile" } },
                { "QcManagement", new List<string> { "/api/dashboards"} },
                { "WarehouseManagement", new List<string> {"/api/WareHouse-Management"} },
                { "LineManagement", new List<string> { "/api/dashboards"} }
            };
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
            string[] segments = requestUri.Split('/');
            string controller = segments.Length > 2 ? $"/api/{segments[2]}" : string.Empty;

            try
            {
                string userRole = Authentication.GetUserRoleFromHttpContext(context);

                // If the user role is admin, allow access to all controllers
                if (userRole == "admin") return true;

                // Check if the user's role has permission to access the requested controller
                if (_rolePermissions.TryGetValue(userRole, out var allowedControllers))
                {
                    return allowedControllers.Any(uri => requestUri.StartsWith(uri, System.StringComparison.OrdinalIgnoreCase));
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking permissions");
                return false;
            }
        }
    }
}
