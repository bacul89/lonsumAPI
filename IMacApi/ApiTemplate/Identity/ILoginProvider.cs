using System.Security.Claims;

namespace ApiTemplate.Identity
{
    public interface ILoginProvider
    {
        bool ValidateCredentials(string userName, string password, out ClaimsIdentity identity,out string role);
    }
}