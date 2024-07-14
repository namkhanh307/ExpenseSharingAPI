using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace Core.Infrastructure
{
    public class Authentication
    {
        public static string GetUserIdFromHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                if (httpContextAccessor.HttpContext == null || !httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    throw new UnauthorizedException("Need Authorization");
                }

                string? authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"];

                if (string.IsNullOrWhiteSpace(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    throw new UnauthorizedException($"Invalid authorization header: {authorizationHeader}");
                }

                string jwtToken = authorizationHeader["Bearer ".Length..].Trim();

                var tokenHandler = new JwtSecurityTokenHandler();

                if (!tokenHandler.CanReadToken(jwtToken))
                {
                    throw new UnauthorizedException("Invalid token format");
                }

                var token = tokenHandler.ReadJwtToken(jwtToken);
                var idClaim = token.Claims.FirstOrDefault(claim => claim.Type == "id");

                return idClaim?.Value ?? throw new UnauthorizedException("Cannot get userId from token");
            }
            catch (UnauthorizedException ex)
            {
                var errorResponse = new
                {
                    data = "An unexpected error occurred.",
                    additionalData = (object)null, // Explicitly setting null type
                    message = ex.Message,
                    statusCode = StatusCodes.Status401Unauthorized,
                    code = "Unauthorized!"
                };

                var jsonResponse = System.Text.Json.JsonSerializer.Serialize(errorResponse);

                if (httpContextAccessor.HttpContext != null)
                {
                    httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    httpContextAccessor.HttpContext.Response.ContentType = "application/json";
                    httpContextAccessor.HttpContext.Response.WriteAsync(jsonResponse).Wait();
                }

                httpContextAccessor.HttpContext?.Response.WriteAsync(jsonResponse).Wait();

                throw; // Re-throw the exception to maintain the error flow
            }
        }
        public static string GetUserIdFromHttpContext(HttpContext httpContext)
        {
            try
            {
                if (!httpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    throw new UnauthorizedException("Need Authorization");
                }

                string? authorizationHeader = httpContext.Request.Headers["Authorization"];

                if (string.IsNullOrWhiteSpace(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    throw new UnauthorizedException($"Invalid authorization header: {authorizationHeader}");
                }

                string jwtToken = authorizationHeader["Bearer ".Length..].Trim();

                var tokenHandler = new JwtSecurityTokenHandler();

                if (!tokenHandler.CanReadToken(jwtToken))
                {
                    throw new UnauthorizedException("Invalid token format");
                }

                var token = tokenHandler.ReadJwtToken(jwtToken);
                var idClaim = token.Claims.FirstOrDefault(claim => claim.Type == "id");

                return idClaim?.Value ?? throw new UnauthorizedException("Cannot get userId from token");
            }
            catch (UnauthorizedException ex)
            {
                var errorResponse = new
                {
                    data = "An unexpected error occurred.",
                    additionalData = (object)null, // Explicitly setting null type
                    message = ex.Message,
                    statusCode = StatusCodes.Status401Unauthorized,
                    code = "Unauthorized!"
                };

                var jsonResponse = System.Text.Json.JsonSerializer.Serialize(errorResponse);

                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.WriteAsync(jsonResponse).Wait();

                throw; // Re-throw the exception to maintain the error flow
            }
        }

        public static string GetUserRoleFromHttpContext(HttpContext httpContext)
        {
            try
            {
                if (!httpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    throw new UnauthorizedException("Need Authorization");
                }

                string? authorizationHeader = httpContext.Request.Headers["Authorization"];

                if (string.IsNullOrWhiteSpace(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    throw new UnauthorizedException($"Invalid authorization header: {authorizationHeader}");
                }

                string jwtToken = authorizationHeader["Bearer ".Length..].Trim();

                var tokenHandler = new JwtSecurityTokenHandler();

                if (!tokenHandler.CanReadToken(jwtToken))
                {
                    throw new UnauthorizedException("Invalid token format");
                }

                var token = tokenHandler.ReadJwtToken(jwtToken);
                var roleClaim = token.Claims.FirstOrDefault(claim => claim.Type == "role");

                return roleClaim?.Value ?? throw new UnauthorizedException("Cannot get user role from token");
            }
            catch (UnauthorizedException ex)
            {
                var errorResponse = new
                {
                    data = "An unexpected error occurred.",
                    additionalData = (object)null, // Explicitly setting null type
                    message = ex.Message,
                    statusCode = StatusCodes.Status401Unauthorized,
                    code = "Unauthorized!"
                };

                var jsonResponse = System.Text.Json.JsonSerializer.Serialize(errorResponse);

                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.WriteAsync(jsonResponse).Wait();

                throw; // Re-throw the exception to maintain the error flow
            }
        }

    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}


