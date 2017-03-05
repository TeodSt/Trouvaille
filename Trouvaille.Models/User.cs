using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Trouvaille.Models
{
    public class User : IdentityUser
    {
        private ICollection<Article> articles; 
        private ICollection<Picture> pictures;

        public User()
        {
            this.articles = new HashSet<Article>();
            this.pictures = new HashSet<Picture>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string LastName { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<Article> LikedArticles
        {
            get { return this.articles; }
            set { this.articles = value; }
        }

        public virtual ICollection<Picture> LikedPictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
