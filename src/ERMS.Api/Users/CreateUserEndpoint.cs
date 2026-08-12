using ERMS.Api.Endpoints;
using ERMS.Api.Extensions;
using ERMS.Domain.Users.CreateUser;
using MediatR;

namespace ERMS.Api.Users
{
    public sealed class CreateUserEndpoint : IEndpoint 
    { 
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/users", HandleAsync).WithName("CreateUser").WithTags("Users"); 
        }
        private static async Task<IResult> HandleAsync(CreateUserCommand command, ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken); return result.ToHttpResult(); 
        } 
    }
}
