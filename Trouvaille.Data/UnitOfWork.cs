using System;
using System.Data.Entity;
using Trouvaille.Data.Contracts;

namespace Trouvaille.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ITrouvailleContext trouvailleContext;

        public UnitOfWork(ITrouvailleContext trouvailleContext)
        {
            this.trouvailleContext = trouvailleContext;
        }
        

        public void Commit()
        {
            this.trouvailleContext.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
