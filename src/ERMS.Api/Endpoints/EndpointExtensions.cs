using ERMS.Api.Users;
using ERMS.Application.Users;
namespace ERMS.Api.Endpoints

{
    public static class EndpointExtensions 
    {
        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app) 
        {
            new CreateUserEndpoint().MapEndpoint(app);
            new GetUserEndpoint().MapEndpoint(app);
            return app;
        }
    }
}
