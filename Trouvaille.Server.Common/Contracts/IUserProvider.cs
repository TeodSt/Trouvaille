namespace Trouvaille.Server.Common.Contracts
{
    public interface IUserProvider
    {
        string Username { get; }

        string UserId { get; }
    }
}
