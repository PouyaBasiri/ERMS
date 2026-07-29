using ERMS.Api.Users;
namespace ERMS.Api.Endpoints

{
    public static class EndpointExtensions 
    {
        public static IServiceCollection AddEndpoints(
         this IServiceCollection services)
        {
            return services;
        }

        public static IEndpointRouteBuilder MapEndpoints(
            this IEndpointRouteBuilder app)
        {
            new CreateUserEndpoint().MapEndpoint(app);

            return app;
        }
    }
}
