using System.Collections.Generic;
using Trouvaille.Models;

namespace Trouvaille.Services.Contracts
{
    public interface IPictureService
    {
        IEnumerable<Picture> GetAllPictures();

        IEnumerable<Picture> GetPicturesByContinent(string continentName);
        
        Picture GetPictureById(int id);

        void AddPicture(Picture picture);

        void DeletePicture(Picture picture);
    }
}
