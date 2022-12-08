namespace OpenPOS_APP.Models;

public class UserRole
{
    public int Role_id { get; set; }

    public int User_id { get; set; }
    
    public UserRole() { }

    public UserRole(int roleId, int userId)
    {
        Role_id = roleId;
        User_id = userId;
    }
}