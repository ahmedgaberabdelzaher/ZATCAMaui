using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.InstalmentPlan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        List<SKS> strList;
        public TestPage()
        {
            InitializeComponent();
            strList = new List<SKS>() { new SKS() { Name = "Sandeep Selected", UName = "Sandeep Unselected" }, new SKS() { Name = "Saini Selected", UName = "Saini Unselected" } };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            lvTest.ItemsSource = strList;
        }
    }
    public class SKS
    {
        public string UName { get; set; }
        public string Name { get; set; }
    }
}