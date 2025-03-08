using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HW01
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Third
    {
        private readonly RequestDelegate _next;

        public Third(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("Middleware #3");
            int number = Convert.ToInt32(httpContext.Session.GetString("Number"));
            return httpContext.Response.WriteAsync($"Your number is {number}");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ThirdExtensions
    {
        public static IApplicationBuilder UseThird(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Third>();
        }
    }
}
