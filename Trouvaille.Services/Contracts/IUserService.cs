using System.Collections.Generic;
using Trouvaille.Models;

namespace Trouvaille.Services.Contracts
{
    public interface IUserService
    {
        User GetUserById(string id);

        IEnumerable<User> GetAllUsers();

        IEnumerable<User> GetUserByUsername(string text);

        void DeleteUser(User user);
    }
}
