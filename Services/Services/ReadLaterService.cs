using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Services
{
     public class ReadLaterService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReadLaterService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        internal string GetAuthenticatedUserUid()
        {
            var userUid = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;

            if (userUid == null)
                throw new System.Exception("UnaauthorizedAction");

            return userUid;
        }

    }
}
