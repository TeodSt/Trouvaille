using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading.Tasks;
using Trouvaille.Models;

namespace Trouvaille.Server.Identity.Contracts
{
    public interface IApplicationSignInManager : IDisposable
    {
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);

        Task SignInAsync(User user, bool isPersistent, bool rememberBrowser);
    }
}
