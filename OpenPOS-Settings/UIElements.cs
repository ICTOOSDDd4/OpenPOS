using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Settings
{
    public class UIElements
    {
      public ResourceDictionary AppColors { get; set; } = new ResourceDictionary();

      public UIElements()
      {
         AppColors.SetAndLoadSource(new Uri("Resources/Styles/Colors.xaml", UriKind.RelativeOrAbsolute), "Resources/Styles/Colors.xaml", this.GetType().GetTypeInfo().Assembly, null);
      }
   }
}
