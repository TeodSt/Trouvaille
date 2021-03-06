﻿using System;
using System.Collections.Generic;
using Bytes2you.Validation;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services.Contracts;

namespace Trouvaille.Services
{
    public class PictureService : IPictureService
    {
        private readonly IEfGenericRepository<Picture> pictureRepository; 
        private readonly IUnitOfWork unitOfWork;
        
        public PictureService(IEfGenericRepository<Picture> pictureRepository, IUnitOfWork unitOfWork)
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
            Guard.WhenArgument(picture, "picture").IsNull().Throw();
            
            using (this.unitOfWork)
            {
                this.pictureRepository.Add(picture);
                this.unitOfWork.Commit();
            }
        }
        
        public IEnumerable<Picture> GetPicturesByContinent(string continentName)
        {
            Guard.WhenArgument(continentName, "continentName").IsNull().Throw();

            IEnumerable<Picture> pictures = this.pictureRepository.GetAll(x => x.Country.Continent.Name == continentName);

            return pictures;
        }

        public IEnumerable<Picture> GetPictureByDescription(string text)
        {
            Guard.WhenArgument(text, "text").IsNull().Throw();
            
            IEnumerable<Picture> pictures = this.pictureRepository.GetAll(x => x.Description.ToLower().Contains(text.ToLower()));

            return pictures;
        }

        public IEnumerable<Picture> GetPicturesByUserId(string userId)
        {
            Guard.WhenArgument(userId, "userId").IsNull().Throw();
            
            IEnumerable<Picture> pictures = this.pictureRepository.GetAll(x => x.CreatorId == userId);

            return pictures;
        }

        public IEnumerable<Picture> GetPicturesByUsername(string username)
        {
            Guard.WhenArgument(username, "username").IsNull().Throw();
            
            IEnumerable<Picture> pictures = this.pictureRepository.GetAll(x => x.Creator.UserName == username);

            return pictures;
        }
    }
}
