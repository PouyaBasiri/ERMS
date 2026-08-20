using ERMS.Api.Endpoints;
using ERMS.Api.Extensions;
using ERMS.Application.Users.GetUsers;
using MediatR;

namespace ERMS.Api.Users
{
    public sealed class GetUsersEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/users",HandleAsync).WithName("GetUsers").WithTags("Users");
        }

        private static async Task<IResult> HandleAsync(ISender sender,CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetUsersQuery(),cancellationToken);

            return result.ToHttpResult();
        }
    }
}
