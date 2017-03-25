using System;
using System.Collections.Generic;
using Bytes2you.Validation;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services.Contracts;
using System.Linq;

namespace Trouvaille.Services
{
    public class CountryService : ICountryService
    {
        private readonly IEfGenericRepository<Country> countryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CountryService(
            IEfGenericRepository<Country> countryRepository,
            IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(countryRepository, "countryRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();

            this.countryRepository = countryRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Country> GetAllCountries()
        {
            IEnumerable<Country> countries = this.countryRepository.GetAll();

            return countries;
        }

        public IEnumerable<Country> GetAllCountriesOrderedByName()
        {
            IEnumerable<Country> countries = this.countryRepository.GetAll().OrderBy(x => x.Name);

            return countries;
        }

        public Country GetCountryById(int id)
        {
            Country country = this.countryRepository.GetById(id);

            return country;
        }
    }
}
