using System.Collections.Generic;
using Bytes2you.Validation;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services.Contracts;

namespace Trouvaille.Services
{
    public class PictureService : IPictureService
    {
        private readonly IGenericRepository<Picture> pictureRepository; 
        private readonly IUnitOfWork unitOfWork;
        
        public PictureService(IGenericRepository<Picture> pictureRepository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(pictureRepository, "pictureRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();

            this.pictureRepository = pictureRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Picture> GetAllPictures()
        {
            IEnumerable<Picture> pictures = this.pictureRepository.GetAll();

            return pictures;
        }

        public Picture GetPictureById(int id)
        {
            Picture picture = this.pictureRepository.GetById(id);

            return picture;
        }

        public void AddPicture(Picture picture)
        {
            using (this.unitOfWork)
            {
                this.pictureRepository.Add(picture);
                this.unitOfWork.Commit();
            }
        }

        public void DeletePicture(Picture picture)
        {
            using (this.unitOfWork)
            {
                this.pictureRepository.Delete(picture);
                this.unitOfWork.Commit();
            }
        }
    }
}
