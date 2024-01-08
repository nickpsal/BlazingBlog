using System.Security.Claims;

namespace BlazingBlog
{
    public static class Extensions
    {
        public static string GetUsername(this ClaimsPrincipal principal) => principal.FindFirstValue(AppConstants.ClaimsNames.FullName)!;
        public static string GetUserID(this ClaimsPrincipal principal) => principal.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}
