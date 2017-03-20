using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using System;

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
            this.InitializeIdentity();
            this.SeedContinets();
            this.SeedCountries();
            this.SeedArticles();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        private void InitializeIdentity()
        {
            

            if (!this.Users.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(this);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var userStore = new UserStore<User>(this);
                var userManager = new UserManager<User>(userStore);

                var userRole = roleManager.FindByName("User");

                if (userRole == null)
                {
                    userRole = new IdentityRole("User");
                    roleManager.Create(userRole);
                }

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

        private void SeedCountries()
        {
            if (!this.Countries.Any())
            {
                var spain = new Country() { Name = "Spain", ContinentId = 3 };
                var bulgaria = new Country() { Name = "Bulgaria", ContinentId = 3 };
                var canada = new Country() { Name = "Canada", ContinentId = 5 };
                var brazil = new Country() { Name = "Brazil", ContinentId = 4 };
                var china = new Country() { Name = "China", ContinentId = 1 };
                var australia = new Country() { Name = "Australia", ContinentId = 6 };
                var kenya = new Country() { Name = "Kenya", ContinentId = 2 };

                this.Countries.Add(spain);
                this.Countries.Add(bulgaria);
                this.Countries.Add(canada);
                this.Countries.Add(brazil);
                this.Countries.Add(china);
                this.Countries.Add(australia);
                this.Countries.Add(kenya);

                this.SaveChanges();
            }
        }

        private void SeedContinets()
        {
            if (!this.Continents.Any())
            {
                var asia = new Continent() { Name = "Asia" };
                var africa = new Continent() { Name = "Africa" };
                var europe = new Continent() { Name = "Europe" };
                var southAmerica = new Continent() { Name = "South America" };
                var northAmerica = new Continent() { Name = "North America" };
                var oceania = new Continent() { Name = "Oceania" };
                var antartica = new Continent() { Name = "Antartica" };

                this.Continents.Add(asia);
                this.Continents.Add(africa);
                this.Continents.Add(europe);
                this.Continents.Add(southAmerica);
                this.Continents.Add(northAmerica);
                this.Continents.Add(oceania);
                this.Continents.Add(antartica);

                this.SaveChanges();
            }
        }

        private void SeedArticles()
        {
            if (!this.Articles.Any())
            {
                var creator = this.Users.FirstOrDefault();

                var articleOne = new Article()
                {
                    Title = "Lorem Ipsum",
                    Subheader = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec non risus quam. Aliquam eget dictum tortor. Nulla imperdiet elementum turpis vitae dapibus. Sed tempor lectus id facilisis molestie. Aenean dignissim vulputate tortor, sed imperdiet est aliquet dictum. Proin eu consectetur dui. Vestibulum ut auctor purus. Donec bibendum, turpis vitae dapibus consequat, magna nisl scelerisque justo, eget iaculis urna sem at lacus.",
                    CreatedOn = DateTime.Now,
                    Creator = creator
                };

                var articleTwo = new Article()
                {
                    Title = "Lorem Ipsum 2",
                    Subheader = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec non risus quam. Aliquam eget dictum tortor. Nulla imperdiet elementum turpis vitae dapibus. Sed tempor lectus id facilisis molestie. Aenean dignissim vulputate tortor, sed imperdiet est aliquet dictum. Proin eu consectetur dui. Vestibulum ut auctor purus. Donec bibendum, turpis vitae dapibus consequat, magna nisl scelerisque justo, eget iaculis urna sem at lacus.",
                    CreatedOn = DateTime.Now,
                    Creator = creator
                };

                this.Articles.Add(articleOne);
                this.Articles.Add(articleTwo);

                this.SaveChanges();
            }
        }
    }
}
