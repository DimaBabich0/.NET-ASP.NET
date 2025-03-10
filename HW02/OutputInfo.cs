using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HW02
{
    public class OutputInfo
    {
        private readonly RequestDelegate _next;

        public OutputInfo(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("Middleware \"OutputInfo\"");
            if (!File.Exists(Config.FilePath))
            {
                return httpContext.Response.WriteAsync("User file not exists");
            }

            string[] lines = File.ReadAllLines(Config.FilePath);
            if (lines.Length == 0)
                return httpContext.Response.WriteAsync("No user information");

            string response = "";
            foreach (string line in lines)
            {
                try
                {
                    User user = User.FromString(line);
                    response += user.ShowInfo() + "\n";
                }
                catch (Exception ex)
                {
                    return httpContext.Response.WriteAsync(ex.Message);
                }
            }
            return httpContext.Response.WriteAsync(response);
        }
    }

    public static class OutputInfoExtensions
    {
        public static IApplicationBuilder UseOutputInfo(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OutputInfo>();
        }
    }
}
