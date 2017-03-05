using System.Collections.Generic;
using Bytes2you.Validation;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services.Contracts;

namespace Trouvaille.Services
{
    public class ContinentService : IContinentService
    {
        private readonly IGenericRepository<Continent> continentRepository;
        private readonly IUnitOfWork unitOfWork;

        public ContinentService(IGenericRepository<Continent> continentRepository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(continentRepository, "continentRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            this.continentRepository = continentRepository;
            this.unitOfWork = unitOfWork;
        }


        public IEnumerable<Continent> GetAllContinents()
        {
            IEnumerable<Continent> continents = this.continentRepository.GetAll();

            return continents;
        }

        public Continent GetContinentById(int id)
        {
            Continent continent = this.continentRepository.GetById(id);

            return continent;
        }
    }
}
