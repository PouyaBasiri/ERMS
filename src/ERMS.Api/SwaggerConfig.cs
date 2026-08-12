
namespace ParaApi.Client.ConfigHelpers
{
    public static class SwaggerConfig
    {
        
        internal static void UseCustomizedSwagger(this IApplicationBuilder app, string baseWebPath)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint($"/swagger/v1/swagger.json", "API V1");
                opt.InjectJavascript("../swagger.js");
            });

        }

    }
}
