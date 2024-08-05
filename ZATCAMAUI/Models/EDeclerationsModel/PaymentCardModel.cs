

using CommunityToolkit.Mvvm.ComponentModel;

namespace ZATCAMAUI.Models.EDeclerationsModel
{
    public class PaymentCardModel : ObservableRecipient
    {
        public PaymentCardModel()
        {
            BackgroundVisaCardImage = "QSelected.png";
            BackgroundAppleImage = "QUnselected.png";
            BackgroundSADADImage = "QUnselected.png";


            VisaCardImage = "paywhiteCard.png";
            AppleImage = "ic_icon_applelogo.png";
            SADADImage = "ColorSadad.png";

            VisaCardTextColor = Colors.White;
            AppleTextColor = Color.FromHex("#002447");
            SADADTextColor = Color.FromHex("#002447");
        }

        Color _VisaCardTextColor;
        public Color VisaCardTextColor { get { return _VisaCardTextColor; } set { _VisaCardTextColor = value; OnPropertyChanged(); } }

        Color _SADADTextColor;
        public Color SADADTextColor { get { return _SADADTextColor; } set { _SADADTextColor = value; OnPropertyChanged(); } }

        Color _AppleTextColor;
        public Color AppleTextColor { get { return _AppleTextColor; } set { _AppleTextColor = value; OnPropertyChanged(); } }

        string _VisaCardImage;
        public string VisaCardImage { get { return _VisaCardImage; } set { _VisaCardImage = value; OnPropertyChanged(); } }

        string _SADADImage;
        public string SADADImage { get { return _SADADImage; } set { _SADADImage = value; OnPropertyChanged(); } }

        string _AppleImage;
        public string AppleImage { get { return _AppleImage; } set { _AppleImage = value; OnPropertyChanged(); } }


        string _BackgroundVisaCardImage;
        public string BackgroundVisaCardImage { get { return _BackgroundVisaCardImage; } set { _BackgroundVisaCardImage = value; OnPropertyChanged(); } }

        string _BackgroundSADADImage;
        public string BackgroundSADADImage { get { return _BackgroundSADADImage; } set { _BackgroundSADADImage = value; OnPropertyChanged(); } }

        string _BackgroundAppleImage;
        public string BackgroundAppleImage { get { return _BackgroundAppleImage; } set { _BackgroundAppleImage = value; OnPropertyChanged(); } }
    }

    public enum PaymentTypes
    {
        Visa = 1,
        SADAD = 2
    }
}

