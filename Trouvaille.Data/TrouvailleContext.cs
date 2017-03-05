using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;

namespace Trouvaille.Data
{
    public class TrouvailleContext : IdentityDbContext<User>, ITrouvailleContext
    {
        public TrouvailleContext() : base("Trouvaille")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TrouvailleContext>());
        }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<Continent> Continents { get; set; }

        public IDbSet<Country> Countries { get; set; }

        public IDbSet<Picture> Pictures { get; set; }

        public IDbSet<Place> Places { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        public static TrouvailleContext Create()
        {
            return new TrouvailleContext();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public void Initialize()
        {
            //this.InitializeIdentity();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Article>().HasRequired(x => x.Creator).WithMany().HasForeignKey(x => x.CreatorId);
            //modelBuilder.Entity<Picture>().HasKey(x => x.CreatorId);
            //modelBuilder.Entity<Place>().HasKey(x => x.FounderId);
        }

        private void InitializeIdentity()
        {
            if (!this.Users.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(this);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var userStore = new UserStore<User>(this);
                var userManager = new UserManager<User>(userStore);

                // Add missing roles
                var role = roleManager.FindByName("Admin");
                if (role == null)
                {
                    role = new IdentityRole("Admin");
                    roleManager.Create(role);
                }

                // Create test users
                var user = userManager.FindByName("admin");
                if (user == null)
                {
                    var newUser = new User()
                    {
                        UserName = "admin",
                        FirstName = "Admin",
                        LastName = "User",
                        Email = "xxx@xxx.net",
                        PhoneNumber = "123456"
                    };

                    userManager.Create(newUser, "unicorn");
                    userManager.SetLockoutEnabled(newUser.Id, false);
                    userManager.AddToRole(newUser.Id, "Admin");
                }
            }
        }
    }
}
