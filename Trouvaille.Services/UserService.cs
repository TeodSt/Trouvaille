using Bytes2you.Validation;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services.Contracts;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Trouvaille.Services
{
    public class UserService : IUserService
    {
        private readonly IEfGenericRepository<User> userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IEfGenericRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(userRepository, "userRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();

            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public void DeleteUser(string id)
        {
            var user = this.GetUserById(id);

            if (user == null)
            {
                throw new ArgumentNullException("User cannot be found");
            }

            using (this.unitOfWork)
            {
                this.userRepository.Delete(user);
                this.unitOfWork.Commit();
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = this.userRepository.GetAll();

            return users;
        }

        public User GetUserById(string id)
        {
            User user = this.userRepository.GetById(id);

            return user;
        }

        public User GetUserByUsername(string text)
        {
            User user = this.userRepository.All.SingleOrDefault(x => x.UserName == text);
            return user;
        }
    }
}
