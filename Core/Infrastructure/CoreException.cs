﻿using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Core.Infrastructure
{
    public class CoreException : Exception
    {
        public CoreException(string code, string message = "", int statusCode = StatusCodes.Status500InternalServerError)
            : base(message)
        {
            Code = code;
            StatusCode = statusCode;
        }


        public string Code { get; }

        public int StatusCode { get; set; }

        [JsonExtensionData] public Dictionary<string, object> AdditionalData { get; set; }

    }
    public class BadRequestException : ErrorException
    {
        public BadRequestException(string errorCode, string message = null)
            : base(400, errorCode, message)
        {
        }
        public BadRequestException(
            ICollection<KeyValuePair<string, ICollection<string>>> errors)
            : base(400, new ErrorDetail
            {
                ErrorCode = "bad_request",
                ErrorMessage = errors
            })
        {
        }
    }

    public class ErrorException : Exception
    {
        public int StatusCode { get; }

        public ErrorDetail ErrorDetail { get; }

        public ErrorException(int statusCode, string errorCode, string message = null)
        {
            StatusCode = statusCode;
            ErrorDetail = new ErrorDetail
            {
                ErrorCode = errorCode,
                ErrorMessage = message
            };
        }

        public ErrorException(int statusCode, ErrorDetail errorDetail)
        {
            StatusCode = statusCode;
            ErrorDetail = errorDetail;
        }
    }

    public class ErrorDetail
    {
        [JsonPropertyName("errorCode")] public string ErrorCode { get; set; }

        [JsonPropertyName("errorMessage")] public object ErrorMessage { get; set; }
    }
    public class ErrorCode
    {
        public const string BadRequest = "Bad Request";
        public const string UnAuthenticated = "Un-Authenticate";
        public const string UnAuthorized = "Forbidden";
        public const string NotFound = "Not Found";
        public const string Unknown = "Oops! Something went wrong, please try again later.";
        public const string NotUnique = "The resource is already, please try another.";
        public const string TokenExpired = "The Token is already expired.";
        public const string TokenInvalid = "The Token is invalid.";
        public const string Validated = "Validated.";
        public const string Conflicted = "Conflicted.";


    }
}