using OpenPOS_Database.Services.Models;
using OpenPOS_Models;

namespace OpenPOS_Database.Factory.Database;

public class TestUserSeeder
{
    private static User[] _users = new User[]
    {
        new User
        {
            Id = 1297,
            Name = "Unit",
            Last_name = "Tester",
            Email = "unit@openpos.org",
            Password = "doeterniettoe"
        },
        new User
        {
            Id = 1298,
            Name = "Unit2",
            Last_name = "Tester2",
            Email = "unit2@openpos.org",
            Password = "doeterniettoe2"
        }
    };

    private static List<User> _dbUsers;

    public static void Seed()
    {
        UserService userService = new UserService();
        _dbUsers = userService.GetAll();
        foreach (User u in _users)
        {
            if (_dbUsers.All(x => x.Email != u.Email))
            {
                userService.Create(u);
            }
        }
    }
}