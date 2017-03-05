using System;

namespace Trouvaille.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
