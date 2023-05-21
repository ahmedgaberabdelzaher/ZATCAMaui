using System;
using GalaSoft.MvvmLight;
using Xamarin.Forms;
namespace EGAZT.Models.EDeclerationsModel
{
	public class PaymentCardModel : ViewModelBase
    {
        public PaymentCardModel()
        {
            BackgroundVisaCardImage = "QSelected.png";
            BackgroundAppleImage = "QUnselected.png";
            BackgroundSADADImage = "QUnselected.png";

            
            VisaCardImage = "paywhiteCard.png";
            AppleImage = "ic_icon_applelogo.png";
            SADADImage = "ColorSadad.png";

            VisaCardTextColor = Color.White;
            AppleTextColor = Color.FromHex("#002447");
            SADADTextColor = Color.FromHex("#002447");
        }

        Color _VisaCardTextColor;
        public Color VisaCardTextColor { get { return _VisaCardTextColor; } set { _VisaCardTextColor = value; RaisePropertyChanged(); } }

        Color _SADADTextColor;
        public Color SADADTextColor { get { return _SADADTextColor; } set { _SADADTextColor = value; RaisePropertyChanged(); } }

        Color _AppleTextColor;
        public Color AppleTextColor { get { return _AppleTextColor; } set { _AppleTextColor = value; RaisePropertyChanged(); } }

        string _VisaCardImage;
        public string VisaCardImage { get { return _VisaCardImage; } set { _VisaCardImage = value; RaisePropertyChanged(); } }

        string _SADADImage;
        public string SADADImage { get { return _SADADImage; } set { _SADADImage = value; RaisePropertyChanged(); } }

        string _AppleImage;
        public string AppleImage { get { return _AppleImage; } set { _AppleImage = value; RaisePropertyChanged(); } }


        string _BackgroundVisaCardImage;
        public string BackgroundVisaCardImage { get { return _BackgroundVisaCardImage; } set { _BackgroundVisaCardImage = value; RaisePropertyChanged(); } }

        string _BackgroundSADADImage;
        public string BackgroundSADADImage { get { return _BackgroundSADADImage; } set { _BackgroundSADADImage = value; RaisePropertyChanged(); } }

        string _BackgroundAppleImage;
        public string BackgroundAppleImage { get { return _BackgroundAppleImage; } set { _BackgroundAppleImage = value; RaisePropertyChanged(); } }
    }

    public enum PaymentTypes
    {
        Visa = 1,
        SADAD = 2
    }
}

