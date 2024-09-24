﻿using Skillup.Shared.Abstractions.Exceptions;
using System.Collections.Concurrent;
using System.Net;

namespace Skillup.Shared.Infrastructure.Exceptions
{
    internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> Codes = new();

        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                SkillupException ex => new ExceptionResponse(new ErrorsResponse(new Error(GetErrorCode(ex), ex.Message))
                    , HttpStatusCode.BadRequest),
                _ => new ExceptionResponse(new ErrorsResponse(new Error("Error", "There was an error.")),
                    HttpStatusCode.InternalServerError)
            };

        private record Error(string Code, string Message);

        private record ErrorsResponse(params Error[] Errors);

        private static string GetErrorCode(object exception)
        {
            var type = exception.GetType();
            return Codes.GetOrAdd(type, type.Name);
        }
    }
}