using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using System.ComponentModel;
using System.Drawing;
using UIKit;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Platforms.iOS.CustomRenderer;

[assembly: ExportRenderer(typeof(GAZTBorderlessEntry), typeof(GAZTBorderlessEntryRenderer))]
namespace ZATCAMAUI.Platforms.iOS.CustomRenderer
{
    public class GAZTBorderlessEntryRenderer : EntryRenderer
    {
        public static void Init() { }
        double fontSize;
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                if (App.IsArabic)
                {
                    Control.TextAlignment = UITextAlignment.Right;
                }
                else
                {
                    Control.TextAlignment = UITextAlignment.Left;
                }

                base.OnElementPropertyChanged(sender, e);
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;

                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
                //Control.TextColor = UIColor.Black;
                AddDoneButton();

            }
            fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
        }
        protected void AddDoneButton()
        {
            var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));

            var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
            {
                this.Control.ResignFirstResponder();
                var baseEntry = this.Element.GetType();
                ((IEntryController)Element).SendCompleted();
            });

            toolbar.Items = new UIBarButtonItem[] {
                new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
                doneButton
            };
            this.Control.InputAccessoryView = toolbar;
        }
    }
}
