using Microsoft.Maui.Controls.Platform;

namespace OpenPOS_APP;

public partial class ProductView : ContentView
{
	public ProductView()
	{
		InitializeComponent();

      MainVerticalLayout.Shadow = new Shadow
      {
         Offset = new Point(10, 10),
         //Radius = 10,
         Brush = Brush.Black,
         Opacity = 0.12f,
       };

      MainVerticalLayout.BackgroundColor = Color.FromRgb(360, 360, 360);
   }
}