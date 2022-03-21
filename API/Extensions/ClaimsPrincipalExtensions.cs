using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user) // u System.Security.Claims dodat cemo metodu GetUsername
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

    }
}
