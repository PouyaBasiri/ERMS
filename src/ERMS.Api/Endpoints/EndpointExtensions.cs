using ERMS.Api.Users;
namespace ERMS.Api.Endpoints

{
    public static class EndpointExtensions 
    {
        public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app) 
        { 
            app.MapUserEndpoints(); return app;
        }
    }
}
