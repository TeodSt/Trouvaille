using Bytes2you.Validation;
using System;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services.Contracts;
using System.Collections.Generic;

namespace Trouvaille.Services
{
    public class UserService : IUserService
    {
        private readonly IEfGenericRepository<User> userRepository;

        public UserService(IEfGenericRepository<User> userRepository)
        {
            Guard.WhenArgument(userRepository, "userRepository").IsNull().Throw();

            this.userRepository = userRepository;
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
    }
}
