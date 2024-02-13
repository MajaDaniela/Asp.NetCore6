using Asp.NetCore6.CustomConstraints;
using Microsoft.VisualBasic;

namespace Asp.NetCore6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRouting(options =>
            {
                options.ConstraintMap.Add("alhanumeric", typeof(AlphaNumericConstraint));
            });
            var app = builder.Build();

            app.UseRouting();

            app.UseEndpoints(endpoint =>
            {
                //"id=1" deafault value is 1
                //"id?" means its optional
                //"id:int" no space between
                //int:min(10):max(1000)
                //int:range(10,1000) between 10 and 1000
                endpoint.MapGet("/products/{id:int:range(10,1000)}", async (context) => // Access /products/101
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
                // alha = text value
                endpoint.MapGet("/books/author/{authorname:alpha:length(4, 8)}/{bookid?}", async (context) =>
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


                //regex constraints - only accept certain values.
                //"/quaterly-reports/{year:int:min(1999):minlength(4)}/{month:regex(^(mar|jun|sep|dec)$)}"
                endpoint.MapGet("/quaterly-reports/{year:int:min(1999):minlength(4)}/{month}", async (context) =>
            {
                int year = Convert.ToInt32(context.Request.RouteValues["year"]);
                string? month = Convert.ToString(context.Request.RouteValues["month"]); //? means its a nullable type
                if (month == "mar" || month == "jun" || month == "sep" || month == "dec")
                {
                    await context.Response.WriteAsync($"This is the quaterly report for {year}-{month}");
                }
                else
                {
                    await context.Response.WriteAsync($"The month value {month} is not valid");
                }


            });
                endpoint.MapGet("/monthly-reports/{month:regex(^([1-9]|1[012])$)}", async (context) =>
                {
                    int monthNumber = Convert.ToInt32(context.Request.RouteValues["month"]);
                    await context.Response.WriteAsync($"This is the monthly report for month number {monthNumber}");
                
            });
                //YYYY-MM-DD OR YYYY.MM.DD OR YYYY/MM/DD - match between 1990-01-01 to 2099-12-31
                // this means two digits "\\"
                endpoint.MapGet("/daily-reports/{date:regex(^(19|20)\\d\\d[-/.](0[1-9]|1[012])[-/.](0[1-9]|[12][0-9]|3[01])$)}", async (context) =>
                {
                    string? date = Convert.ToString(context.Request.RouteValues["date"]);
                    await context.Response.WriteAsync($"This is the monthly report for month number {date}");
                });

                //custom Route Constraint
                endpoint.MapGet("/user/{username:alhanumeric}", async (context) =>
                {
                    string? username = Convert.ToString(context.Request.RouteValues["username"]);
                    await context.Response.WriteAsync($"Welcome {username}");
                });


            });
            //This run when there is no specific route to run.
            app.Run(async (HttpContext context) =>
                {
                    await context.Response.WriteAsync("The URL which you are looking for is not found!");
                });

                app.Run();            
        }
    }
}