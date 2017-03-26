using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using Trouvaille.Models;

namespace Trouvaille.Server.Identity.Contracts
{
    public interface IApplicationUserManager : IDisposable
    {
        Task<IdentityResult> CreateAsync(User user, string password);
    }
}
