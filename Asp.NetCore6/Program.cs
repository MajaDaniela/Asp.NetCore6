using Microsoft.VisualBasic;

namespace Asp.NetCore6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseRouting();

            app.UseEndpoints(endpoint =>
            {
                //? means its optional
                endpoint.MapGet("/products/{id?}", async (context) => // Access /products/101
                {
                    var id = context.Request.RouteValues["ID"];
                    if (id != null)
                    {
                        id = Convert.ToInt32(id);
                        await context.Response.WriteAsync("This is procuct with ID " + id);
                    }
                    else
                    {
                        await context.Response.WriteAsync("You are in the products page!");
                    }                                       
                });
                //default bookid is 1. If no value is selected it will run the last route.
                //default value will be ignored if a value is specified by the user
                endpoint.MapGet("/books/author/{authorname=John}/{bookid=1}", async (context) =>
                {
                    var BookId = Convert.ToInt32(context.Request.RouteValues["bookid"]);
                    var AuthorName = Convert.ToInt32(context.Request.RouteValues["authorname"]);
                    await context.Response.WriteAsync($"This is the book authored by {AuthorName} and ");
                });
            });
            //This run when there is no specific route to run.
                app.Run(async (HttpContext context) =>
                {
                    await context.Response.WriteAsync("Welcome to ASP.NET Core app");
                });

                app.Run();            
        }
    }
}