using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using System.Web;
using Trouvaille.Server.Common.Contracts;

namespace Trouvaille.Server.Common
{
    public class UserProvider : IUserProvider
    {
        private readonly HttpContextBase context;

        public UserProvider(HttpContextBase context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public string Username
        {
            get
            {
                return this.context.User.Identity.Name;
            }
        }

        public string UserId
        {
            get
            {
                return this.context.User.Identity.GetUserId();
            }
        }
    }
}
