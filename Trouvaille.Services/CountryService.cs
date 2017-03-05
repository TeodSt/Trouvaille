﻿using System.Collections.Generic;
using Bytes2you.Validation;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services.Contracts;

namespace Trouvaille.Services
{
    public class CountryService : ICountryService
    {
        private readonly IGenericRepository<Country> countryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CountryService(IGenericRepository<Country> countryRepository, IUnitOfWork unitOfWork)
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

        public Country GetCountryById(int id)
        {
            Country country = this.countryRepository.GetById(id);

            return country;
        }
    }
}
