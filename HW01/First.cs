using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HW01
{
    public class First
    {
        private readonly RequestDelegate _next;

        public First(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("Middleware #1");

            string? token = httpContext.Request.Query["Number"];
            if (string.IsNullOrEmpty(token))
            {
                return httpContext.Response.WriteAsync("Incorrect parameter");
            }    

            int number = Convert.ToInt32(token);
            number = Math.Abs(number);
            httpContext.Session.SetString("Number", number.ToString());
            return _next(httpContext);
        }
    }

    public static class FirstExtensions
    {
        public static IApplicationBuilder UseFirst(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<First>();
        }
    }
}
