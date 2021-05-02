using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
namespace Common.CurrentUser
{
    public static class UserClaimsExtension
    {
        public static string GetUsername(this IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(x => x.Type == "Username").Value;
        }

        public static Guid GetUserId(this IEnumerable<Claim> claims)
        {
            return Guid.Parse(claims.FirstOrDefault(x => x.Type == "UserId").Value);
        }
    }
}