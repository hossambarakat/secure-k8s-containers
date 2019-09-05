using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.Configure(app =>
                {
                    app.UseDefaultFiles();
                    app.UseStaticFiles();
                    app.UseRouting();
                    app.UseEndpoints(route =>
                    {
                        route.MapGet("/", context => context.Response.WriteAsync("Hello world"));
                    });
                });
            })
            .Build().Run();
    }
}
