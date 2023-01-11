using Microsoft.Extensions.Configuration;
using System.Reflection;
using OpenPOS_Database.Factory.Database;
using OpenPOS_Database.ModelServices;

namespace OpenPOS_Testing
{
    [TestFixture]
    public class DatabaseSeederTest
    {
        private RoleService _roleService = new();
        [SetUp]
        public void SetUp()
        {
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("OpenPOS_Testing.appsettings.test.json");

            if (stream != null)
            {
                var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

                ApplicationSettings.DbSett = config.GetRequiredSection("DATABASE_CONNECTION").Get<DatabaseSettings>();

                if (ApplicationSettings.DbSett != null)
                {
                    System.Diagnostics.Debug.WriteLine(ApplicationSettings.DbSett.connection_string);
                }
            }
        }

        [Test]
        public void DatabaseSeeder_A_Initilize_ReturnsNoException()
        {
            DatabaseService.SetConnectionString();
            Seeder.Initialize();
        }
        [Test]
        public void DatabaseSeeder_CheckRoles_ReturnsRoles()
        {
            DatabaseService.SetConnectionString();
            List<Role> roles = _roleService.GetAll();
            List<string> expectedRoles = new List<string>() { "Owner", "Admin", "Crew", "Cook", "Bar", "Guest" };

            foreach (string role in expectedRoles)
            {
                if (roles.All(r => r.Title != role))
                {
                    Assert.Fail($"{role} does not exist");
                }
            }
        }
    }
}
