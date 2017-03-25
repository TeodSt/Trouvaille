using Bytes2you.Validation;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System;

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

        public void DeleteUser(string userId)
        {
            Guard.WhenArgument(userId, "userId").IsNull().Throw();

            var user = this.GetUserById(userId);
            Guard.WhenArgument(user, "User cannot be found").IsNull().Throw();

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
            Guard.WhenArgument(id, "id").IsNull().Throw();
            User user = this.userRepository.GetById(id);

            return user;
        }

        public User GetUserByUsername(string username)
        {
            Guard.WhenArgument(username, "username").IsNull().Throw();
            User user = this.userRepository.All.SingleOrDefault(x => x.UserName == username);

            return user;
        }

        public IEnumerable<User> GetAllUsersByUsername(string username)
        {
            Guard.WhenArgument(username, "username").IsNull().Throw();
            IEnumerable<User> users = this.userRepository.GetAll(x => x.UserName.ToLower().Contains(username.ToLower()));

            return users;
        }
    }
}
