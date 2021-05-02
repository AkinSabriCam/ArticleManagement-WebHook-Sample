using Microsoft.AspNetCore.Http;

namespace Common.CurrentUser
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser _currentUser;

        public UserProvider(IUser currentUser, IHttpContextAccessor httpContextAccessor)
        {
            _currentUser = currentUser;
            _httpContextAccessor = httpContextAccessor;
        }


        public IUser GetCurrentUser()
        {
            if (UserLocalContext.Instance.GetUser() != null)
                return UserLocalContext.Instance.GetUser();

            if (_httpContextAccessor.HttpContext != null && _currentUser.IsAuthenticated())
                return _currentUser;

            return null;
        }
    }
}