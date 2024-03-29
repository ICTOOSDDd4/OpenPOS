﻿using OpenPOS_Models;

namespace OpenPOS_Settings;

public static class ApplicationSettings
{
   public static DatabaseSettings DbSett { get; set; }
   public static ApiSettings ApiSet { get; set; }
   public static TikkieSettings TikkieSet { get; set; }
   public static QRCodeGeneratorSettings QRCodeGeneratorSet { get; set; }
   public static Dictionary<Product, int> CheckoutList { get; set; } = new Dictionary<Product, int>();
   public static User LoggedinUser { get; set; }
   public static Bill CurrentBill { get; set; }
   public static int TableNumber { get; set; }
}