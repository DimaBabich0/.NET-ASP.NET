using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HW01
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Second
    {
        private readonly RequestDelegate _next;

        public Second(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("Middleware #2");

            int number = Convert.ToInt32(httpContext.Session.GetString("Number"));

            if (number < 1)
            {
                return httpContext.Response.WriteAsync("Number less than 1");
            }
            else if (number > 100000)
            {
                return httpContext.Response.WriteAsync("Number greater than 100000");
            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SecondExtensions
    {
        public static IApplicationBuilder UseSecond(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Second>();
        }
    }
}
