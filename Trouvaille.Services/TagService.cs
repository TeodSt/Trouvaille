using System.Collections.Generic;
using Bytes2you.Validation;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services.Contracts;

namespace Trouvaille.Services
{
    public class TagService : ITagService
    {
        private readonly IEfGenericRepository<Tag> tagRepository;
        private readonly IUnitOfWork unitOfWork;

        public TagService(IEfGenericRepository<Tag> tagRepository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(tagRepository, "tagRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();

            this.tagRepository = tagRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            IEnumerable<Tag> tags = this.tagRepository.GetAll();

            return tags;
        }

        public void CreateTag(Tag tag)
        {
            using (this.unitOfWork)
            {
                this.tagRepository.Add(tag);
                this.unitOfWork.Commit();
            }
        }
    }
}
