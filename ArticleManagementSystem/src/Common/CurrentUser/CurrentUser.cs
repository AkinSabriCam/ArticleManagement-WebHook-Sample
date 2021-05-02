using System;
using Microsoft.AspNetCore.Http;

namespace Common.CurrentUser
{
    public class CurrentUser : IUser
    {
        private readonly HttpContext _httpContext;
        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            _httpContext = contextAccessor.HttpContext;
        }

        public Guid UserId
        {
            get
            {
                if (IsAuthenticated())
                    return _httpContext.User.Claims.GetUserId();

                return UserLocalContext.Instance.GetUserId();
            }
        }
        public string Username
        {
            get
            {
                if (IsAuthenticated())
                    return _httpContext.User.Claims.GetUsername();

                return UserLocalContext.Instance.GetUsername();
            }
        }

        public bool IsAuthenticated()
        {
            return _httpContext.User.Identity.IsAuthenticated;
        }
    }
}