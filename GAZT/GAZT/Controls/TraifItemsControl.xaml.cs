using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EGAZT.Controls
{
    public partial class TraifItemsControl : ContentView
    {
        public TraifItemsControl()
        {
            InitializeComponent();
            if (!App.IsArabic)
            {
              arrow.Rotation=0;


            }
            else
            {
                arrow.Rotation = 180;


                // Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
        }


       

    }
}
