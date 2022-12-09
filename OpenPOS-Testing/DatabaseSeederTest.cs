using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenPOS_APP.Enums;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Settings;
using OpenPOS_Database;
using OpenPOS_Database.Factory.Database;
using OpenPOS_Database.Services.Models;
using OpenPOS_Settings;

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
            else throw new Exception();
        }

        [Test]
        public void DatabaseSeeder_A_Initilize_ReturnsNoException()
        {
            try
            {
                DatabaseService.SetConnectionString();
                Seeder.Initialize();
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
        [Test]
        public void DatabaseSeeder_CheckRoles_ReturnsRoles()
        {
            try
            {
                DatabaseService.SetConnectionString();
                List<Role> roles = _roleService.GetAll();
                List<string> ExpectedRoles = new List<string>() { "Owner", "Admin", "Crew", "Cook", "Bar", "Guest" };

                foreach (string role in ExpectedRoles)
                {
                    if (roles.All(r => r.Title != role))
                    {
                        Assert.Fail($"{role} does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
