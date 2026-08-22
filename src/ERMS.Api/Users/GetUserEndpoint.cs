using ERMS.Api.Endpoints;
using ERMS.Api.Extensions;
using ERMS.Application.Users.GetUser;
using ERMS.SharedKernel.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ERMS.Application.Users
{
    public sealed class GetUserEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/users/{id:guid}",HandleAsync).WithName("GetUser").WithTags("Users");
        }

        private static async Task<IResult> HandleAsync(Guid id,ISender sender,CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new GetUserQuery(id),cancellationToken);

            return result.ToHttpResult();
        }
    }
}
