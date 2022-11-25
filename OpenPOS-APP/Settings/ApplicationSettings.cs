using OpenPOS_APP.Models;
using System.Reflection;

namespace OpenPOS_APP.Settings;

public static class ApplicationSettings
{
    public static DatabaseSettings DbSett { get; set; }
    public static TikkieSettings TikkieSet { get; set; }
   public static User LoggedinUser { get; set; }
   public static UIElements UIElements { get; set; }

}