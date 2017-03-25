using System.Collections.Generic;
using Trouvaille.Models;

namespace Trouvaille.Services.Contracts
{
    public interface IUserService
    {
        User GetUserById(string id);

        IEnumerable<User> GetAllUsers();

        IEnumerable<User> GetAllUsersByUsername(string username);

        User GetUserByUsername(string username);

        void DeleteUser(string id);
    }
}
