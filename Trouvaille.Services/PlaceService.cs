﻿using System.Collections.Generic;
using Bytes2you.Validation;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services.Contracts;

namespace Trouvaille.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IGenericRepository<Place> placeRepository;
        private readonly IUnitOfWork unitOfWork;

        public PlaceService(IGenericRepository<Place> placeRepository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(placeRepository, "placeRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            this.placeRepository = placeRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Place> GetAllPlaces()
        {
            IEnumerable<Place> places = this.placeRepository.GetAll();

            return places;
        }

        public Place GetPlaceById(int id)
        {
            Place place = this.placeRepository.GetById(id);

            return place;
        }

        public void AddPlace(Place place)
        {
            using (this.unitOfWork)
            {
                this.placeRepository.Add(place);
                unitOfWork.Commit();
            }
        }

        public void DeletePlace(Place place)
        {
            using (this.unitOfWork)
            {
                this.placeRepository.Delete(place);
                unitOfWork.Commit();
            }
        }
    }
}