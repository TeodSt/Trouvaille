using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Trouvaille.Models
{
    public class Post
    {
        public int Id { get; set; }

        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }
    }
}
