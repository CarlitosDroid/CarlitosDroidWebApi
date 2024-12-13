using System.Security.Claims;

namespace CarlitosDroidWebApi.Claims;

public class AppUserClaimsPrincipal : ClaimsPrincipal
{
    public AppUserClaimsPrincipal(IHttpContextAccessor contextAccessor) : base(contextAccessor.HttpContext.User) { }

    public string Id => FindFirst(ClaimTypes.NameIdentifier).Value;
    public string Email => FindFirst(ClaimTypes.Email).Value;
}