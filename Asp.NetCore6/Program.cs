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
                //"id=1" deafault value is 1
                //"id?" means its optional
                //"id:int" no space between
                endpoint.MapGet("/products/{id:int}", async (context) => // Access /products/101
                {
                    var id = context.Request.RouteValues["ID"]; //this return as an object
                    if (id != null)
                    {
                        id = Convert.ToInt32(id); //this returns int
                        await context.Response.WriteAsync("This is procuct with ID " + id);
                    }
                    else
                    {
                        await context.Response.WriteAsync("You are in the products page!");
                    }                                       
                });
                //default bookid is 1. If no value is selected it will run the last route.
                //default value will be ignored if a value is specified by the user
                endpoint.MapGet("/books/author/{authorname:alpha}/{bookid?}", async (context) =>
                {
                    var BookId = context.Request.RouteValues["bookid"];
                    var AuthorName = Convert.ToString(context.Request.RouteValues["authorname"]);


                    if (BookId != null)
                    {
                        await context.Response.WriteAsync($"This is the book authored by {AuthorName} and book ID is {BookId}");
                    }
                    else 
                    {
                        await context.Response.WriteAsync($"Following are the list of books authored by {AuthorName}");
                    }
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