using System.Collections;
using System.Collections.Generic;
using Trouvaille.Models;

namespace Trouvaille.Services.Contracts
{
    public interface IUserService
    {
        User GetUserById(string id);

        IEnumerable<User> GetAllUsers();
    }
}
