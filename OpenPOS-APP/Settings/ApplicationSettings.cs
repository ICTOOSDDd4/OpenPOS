using OpenPOS_APP.Models;

namespace OpenPOS_APP.Settings;

public static class ApplicationSettings
{
   public static DatabaseSettings DbSett { get; set; }
   public static TikkieSettings TikkieSet { get; set; }
   
   public static QRCodeGeneratorSettings QRCodeGeneratorSet { get; set; }
   public static Dictionary<Product, int> CheckoutList { get; set; } = new Dictionary<Product, int>();
   public static User LoggedinUser { get; set; }
   public static UIElements UIElements { get; set; }
   public static Bill CurrentBill { get; set; }
   public static int TableNumber { get; set; }
}