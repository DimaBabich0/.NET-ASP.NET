using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HW02
{
    public class SignIn
    {
        private readonly RequestDelegate _next;

        public SignIn(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("Middleware \"SignIn\"");

            string? login = httpContext.Request.Query["Login"];
            string? password = httpContext.Request.Query["Password"];

            if (string.IsNullOrEmpty(login) ||
               string.IsNullOrEmpty(password))
                return _next(httpContext);

            string[] lines = File.ReadAllLines(Config.FilePath);
            foreach (string line in lines)
            {
                try
                {
                    User user = User.FromString(line);
                    if (user.Login == login && user.Password == password)
                    {
                        return httpContext.Response.WriteAsync("Authorization was successful!");
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return httpContext.Response.WriteAsync("Authorization failed!\nLogin and/or password is incorrect");
        }
    }

    public static class SignInExtensions
    {
        public static IApplicationBuilder UseSignIn(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SignIn>();
        }
    }
}
