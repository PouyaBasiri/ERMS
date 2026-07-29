using ERMS.Api.Endpoints;
using ERMS.Domain.Users.CreateUser;
using MediatR;

namespace ERMS.Api.Users
{
    public static class CreateUserEndpoint 
    { 
        public static void Map(RouteGroupBuilder group) 
        { 
            group.MapPost("/", HandleAsync).WithName("CreateUser");
        } 
        private static async Task<IResult> HandleAsync(CreateUserCommand command, ISender sender, CancellationToken cancellationToken) 
        { 
            var result = await sender.Send(command, cancellationToken); return Results.Ok(result);
        }
    }
}
