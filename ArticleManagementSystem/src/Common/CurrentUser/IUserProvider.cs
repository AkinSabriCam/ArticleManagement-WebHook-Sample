namespace Common.CurrentUser
{
    public interface IUserProvider
    {
        IUser GetCurrentUser();
    }
}