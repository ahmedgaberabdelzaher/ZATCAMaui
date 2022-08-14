using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListBottomSheet : ContentView
    {
        public ListBottomSheet()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

                
            }
        }
    }
}