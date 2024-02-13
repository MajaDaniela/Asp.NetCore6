using Microsoft.Extensions.FileProviders;

namespace ServeStaticFiles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
            { 
            WebRootPath = "staticfiles"
            
            });
         
            var app = builder.Build();

            //Content root path - C:\ 
            app.UseStaticFiles(new StaticFileOptions()

            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "mywebroot"))
            });

            app.MapGet("/", () => "Hello, World!");

            app.Run();
        }
    }
}