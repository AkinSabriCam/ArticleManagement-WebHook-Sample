using System;

namespace Common.CurrentUser
{
    public interface IUser
    {
        Guid UserId { get; }
        string Username { get; }
        bool IsAuthenticated();
    }
}