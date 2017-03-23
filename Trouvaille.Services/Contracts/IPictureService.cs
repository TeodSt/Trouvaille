using System.Collections.Generic;
using Trouvaille.Models;

namespace Trouvaille.Services.Contracts
{
    public interface IPictureService
    {
        IEnumerable<Picture> GetAllPictures();

        IEnumerable<Picture> GetPicturesByContinent(string continentName);

        IEnumerable<Picture> GetPictureByDescription(string text);

        IEnumerable<Picture> GetPicturesByUserId(string id);

        Picture GetPictureById(int id);

        void AddPicture(Picture picture);

        void DeletePicture(Picture picture);
    }
}
