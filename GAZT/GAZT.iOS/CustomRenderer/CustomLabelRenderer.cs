using System;
using GAZT;
using GAZT.iOS.CustomRenderer;
using Foundation;
using Xamarin.Forms.Internals;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using EGAZT;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class CustomLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            try
            {
                if (Control != null)
                {
                    if (App.IsArabic)
                    {
                        Control.TextAlignment = UITextAlignment.Right;
                        if (e.NewElement != null)
                        {
                            string StyleId = e.NewElement.StyleId;

                            if (!string.IsNullOrEmpty(StyleId))
                            {
                                if (StyleId.Equals("ValueLabel"))
                                {
                                    Control.TextAlignment = UITextAlignment.Left;
                                }
                                if (StyleId.Equals("SingleLabel"))
                                {
                                    Control.TextAlignment = UITextAlignment.Right;
                                }
                            }
                        }
                    }
                    else
                    {
                        Control.TextAlignment = UITextAlignment.Left;
                        if (e.NewElement != null)
                        {
                            string StyleId = e.NewElement.StyleId;

                            if (!string.IsNullOrEmpty(StyleId))
                            {
                                if (StyleId.Equals("ValueLabel"))
                                {
                                    Control.TextAlignment = UITextAlignment.Right;
                                }
                                if (StyleId.Equals("SingleLabel"))
                                {
                                    Control.TextAlignment = UITextAlignment.Left;
                                }
                            }
                        }
                    }

                    if (e.NewElement != null)
                    {
                        string StyleId = e.NewElement.StyleId;

                        if (!string.IsNullOrEmpty(StyleId))
                        {
                            if (StyleId.Equals("OnBoardText"))
                            {
                                Control.TextAlignment = UITextAlignment.Center;
                            }
                        }
                    }

                    if (e.NewElement != null)
                    {
                        string StyleId = e.NewElement.StyleId;

                        if (!string.IsNullOrEmpty(StyleId))
                        {
                            if (StyleId.Equals("LTRLabelText"))
                            {
                                Control.TextAlignment = UITextAlignment.Left;
                            }
                        }
                    }


                    //Line Spacing between two line in one Label
                    //if (!string.IsNullOrWhiteSpace(Element.Text))
                    //{
                    //    var lineSpacingLabel = (CustomLabel)this.Element;
                    //    var paragraphStyle = new NSMutableParagraphStyle()
                    //    {
                    //        LineSpacing = (nfloat)lineSpacingLabel.LineSpacing
                    //    };
                    //    var text = new NSMutableAttributedString(lineSpacingLabel.Text);
                    //    var style = UIStringAttributeKey.ParagraphStyle;
                    //    var range = new NSRange(0, text.Length);

                    //    text.AddAttribute(style, paragraphStyle, range);

                    //    this.Control.AttributedText = text;
                    //}
                    //Line Spacing between two line in one Label


                    //    Control.Font = UIFont.GetPreferredFontForTextStyle(new NSString("UICTFontTextStyleBody"));
                    //    if (e.NewElement != null)
                    //    {
                    //        string StyleId = e.NewElement.StyleId;
                    //        if (!string.IsNullOrEmpty(StyleId))
                    //        {
                    //            if (StyleId.Equals("Medium"))
                    //            {
                    //                var fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                    //                if (App.IsArabic)
                    //                    this.Control.Font = UIFont.FromName("Cairo-Regular", (float)fontSize);
                    //                else
                    //                    this.Control.Font = UIFont.FromName("Helvetica-Normal", (float)fontSize);
                    //            }
                    //            else if (StyleId.Equals("SmallBold"))
                    //            {
                    //                var fontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                    //                if (App.IsArabic)
                    //                    this.Control.Font = UIFont.FromName("Cairo-Regular", (float)fontSize);
                    //                else
                    //                    this.Control.Font = UIFont.FromName("Helvetica-Normal", (float)fontSize);
                    //            }
                    //            else if (StyleId.Equals("TwoLineText"))
                    //            {
                    //                this.Control.Lines = 2;
                    //                this.Control.LineBreakMode = UILineBreakMode.TailTruncation;
                    //                var fontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                    //                if (App.IsArabic)
                    //                    this.Control.Font = UIFont.FromName("Cairo-Light", (float)fontSize);
                    //                else
                    //                    this.Control.Font = UIFont.FromName("HelvLight", (float)fontSize);
                    //            }
                    //            else if (StyleId.Equals("FourLineText"))
                    //            {
                    //                if (Device.Idiom == TargetIdiom.Phone)
                    //                {
                    //                    if (App.IsArabic)
                    //                        this.Control.Lines = 3;
                    //                    else
                    //                        this.Control.Lines = 6;
                    //                }
                    //                else
                    //                {
                    //                    if (App.IsArabic)
                    //                        this.Control.Lines = 5;
                    //                    else
                    //                        this.Control.Lines = 8;
                    //                }
                    //                this.Control.LineBreakMode = UILineBreakMode.TailTruncation;
                    //                var fontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                    //                if (App.IsArabic)
                    //                    this.Control.Font = UIFont.FromName("Cairo-Light", (float)fontSize);
                    //                else
                    //                    this.Control.Font = UIFont.FromName("HelvLight", (float)fontSize);
                    //            }
                    //            else if (StyleId.Equals("Micro"))
                    //            {
                    //                var fontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
                    //                if (App.IsArabic)
                    //                    this.Control.Font = UIFont.FromName("Cairo-Light", (float)fontSize);
                    //                else
                    //                    this.Control.Font = UIFont.FromName("HelvLight", (float)fontSize);
                    //            }
                    //            else if (StyleId.Equals("MicroTabBar"))
                    //            {
                    //                var fontSize = 12;
                    //                if (App.IsArabic)
                    //                {
                    //                    fontSize = 10;
                    //                    this.Control.Font = UIFont.FromName("Cairo-Light", (float)fontSize);
                    //                }
                    //                else
                    //                {
                    //                    fontSize = 10;
                    //                    this.Control.Font = UIFont.FromName("HelvLight", (float)fontSize);
                    //                }
                    //            }
                    //            else if (StyleId.Equals("MicroTableText"))
                    //            {
                    //                var fontSize = 11;
                    //                if (App.IsArabic)
                    //                {
                    //                    fontSize = 11;
                    //                    this.Control.Font = UIFont.FromName("Cairo-Light", (float)fontSize);
                    //                }
                    //            }
                    //            else
                    //            {
                    //                var fontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                    //                if (App.IsArabic)
                    //                {
                    //                    this.Control.Font = UIFont.FromName("Cairo-Light", (float)fontSize);
                    //                    //this.Control.TextAlignment = UITextAlignment.Right;
                    //                }
                    //                else
                    //                    this.Control.Font = UIFont.FromName("HelvLight", (float)fontSize);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            var fontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                    //            if (App.IsArabic)
                    //            {
                    //                this.Control.Font = UIFont.FromName("Cairo-Light", (float)fontSize);
                    //                //this.Control.TextAlignment = UITextAlignment.Right;
                    //            }
                    //            else
                    //                this.Control.Font = UIFont.FromName("HelvLight", (float)fontSize);
                    //        }
                    //    }
                }
            }
            catch (Exception Ex)
            {

            }

        }
    }
}


