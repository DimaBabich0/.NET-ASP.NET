using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HW02
{
    public class SignUp
    {
        private readonly RequestDelegate _next;

        public SignUp(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("Middleware \"SignUp\"");

            string? name = httpContext.Request.Query["Name"];
            string? login = httpContext.Request.Query["Login"];
            string? password = httpContext.Request.Query["Password"];

            if (string.IsNullOrEmpty(name) ||
               string.IsNullOrEmpty(login) ||
               string.IsNullOrEmpty(password))
                return _next(httpContext);

            try
            {
                User user = new User(name, login, password);
                File.AppendAllText(Config.FilePath, user.ToString() + Environment.NewLine);
                return httpContext.Response.WriteAsync("Registration was successful!");
            }
            catch (Exception ex)
            {
                return httpContext.Response.WriteAsync(ex.Message);
            }
        }
    }

    public static class SignUpExtensions
    {
        public static IApplicationBuilder UseSignUp(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SignUp>();
        }
    }
}
