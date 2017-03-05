using System.Collections.Generic;
using Trouvaille.Models;

namespace Trouvaille.Services.Contracts
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAllTags();

        void CreateTag(Tag tag);
    }
}
