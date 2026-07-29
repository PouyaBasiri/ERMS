namespace ERMS.Api.Users
{
    public static class UserEndpoints 
    { 
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app) 
        {
            var group = app.MapGroup("/api/users").WithTags("Users");
            CreateUserEndpoint.Map(group); return app; 
        }
    }
}
