using System;
using System.Threading;

namespace Common.CurrentUser
{
    public class UserLocalContext
    {
        public static readonly UserLocalContext Instance = new UserLocalContext();
        private static AsyncLocal<IUser> User;

        public void SetUser(IUser user)
        {
            if (User == null)
                User = new AsyncLocal<IUser>();

            User.Value = user;
        }

        public IUser GetUser()
        {
            return User?.Value;
        }

        public string GetUsername()
        {
            return User?.Value.Username;
        }

        public Guid GetUserId()
        {
            return (Guid)(User?.Value.UserId);
        }
    }
}