using System.Collections.Generic;
using Bytes2you.Validation;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services.Contracts;

namespace Trouvaille.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IEfGenericRepository<Place> placeRepository;
        private readonly IUnitOfWork unitOfWork;

        public PlaceService(IEfGenericRepository<Place> placeRepository, IUnitOfWork unitOfWork)
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

        public void AddPlace(Place place)
        {
            Guard.WhenArgument(place, "place").IsNull().Throw();
            
            using (this.unitOfWork)
            {
                this.placeRepository.Add(place);
                this.unitOfWork.Commit();
            }
        }
    }
}
