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

        IEnumerable<Picture> GetPicturesByUsername(string username);

        Picture GetPictureById(int id);

        void AddPicture(Picture picture);        
    }
}
