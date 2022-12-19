using System.Reflection;
using OpenPOS_Models;
using OpenPOS_Settings;
using OpenPOS_Settings.EventArgsClasses;
using Brush = Microsoft.Maui.Controls.Brush;
using Shadow = Microsoft.Maui.Controls.Shadow;

namespace OpenPOS_APP;

public partial class AdminCategoryView : ContentView
{
    private AdminCategoryPage _adminCategoryPage;
    private Category _category;
    private readonly ResourceDictionary _appColors = new();

    public AdminCategoryView()
    {
        InitializeComponent();
        MainVerticalLayout.Shadow = new Shadow
        {
            Offset = new Point(5, 5),
            Brush = Brush.Black,
            Opacity = 0.12f,
        };
        _appColors.SetAndLoadSource(new Uri("Resources/Styles/Colors.xaml", UriKind.RelativeOrAbsolute), "Resources/Styles/Colors.xaml", this.GetType().GetTypeInfo().Assembly, null);
    }

    public void SetCategoryValue(AdminCategoryPage page, Category category)
    {
        _adminCategoryPage = page;
        _category = category;
        
        CategoryName.Text = category.Name;
    }
}
