using System;
using System.Windows.Input;
using Xamarin.Forms;
using EGAZT.Models.EDeclerationsModel;
using System.Collections.Generic;
using System.Linq;
using EGAZT.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;
using EGAZT.Models.EDeclerationsModel.SubmitModels;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
	public class EDeclarationPaymentViewModel : BaseViewModel
    {
        TripCardModel paymentCard = new TripCardModel();
        public TripCardModel PaymentCard { get { return paymentCard; } set { paymentCard = value; } }

        TravelerDeclarationResponse _TravelerDeclarationResponse = new TravelerDeclarationResponse();
        public TravelerDeclarationResponse TravelerDeclarationResponse { get { return _TravelerDeclarationResponse; } set { _TravelerDeclarationResponse = value; RaisePropertyChanged(); } }

        public PaymentTypes SelctedPaymentType { get; set; }

        public ICommand PaymentCardCommand
        {
            get
            {
                return new Command<string>((e) =>
                {
                    var selectedTrip = int.Parse(e);

                    if (selectedTrip == (int)TripName.AirTrip)
                    {
                        PaymentCard.AirImage = "QSelected.png";
                        PaymentCard.SeaImage = "QUnselected.png";
                        PaymentCard.LandImage = "QUnselected.png";

                        PaymentCard.AirTextColor = Color.White;
                        PaymentCard.SeaTextColor = Color.FromHex("#002447");
                        PaymentCard.LandTextColor = Color.FromHex("#002447");
                    }

                    else if (selectedTrip == (int)TripName.LandTrip)
                    {
                        PaymentCard.AirImage = "QUnselected.png";
                        PaymentCard.SeaImage = "QUnselected.png";
                        PaymentCard.LandImage = "QSelected.png";

                        PaymentCard.AirTextColor = Color.FromHex("#002447");
                        PaymentCard.SeaTextColor = Color.FromHex("#002447");
                        PaymentCard.LandTextColor = Color.White;
                    }
                    else
                    {
                        PaymentCard.AirImage = "QUnselected.png";
                        PaymentCard.SeaImage = "QSelected.png";
                        PaymentCard.LandImage = "QUnselected.png";

                        PaymentCard.AirTextColor = Color.FromHex("#002447");
                        PaymentCard.SeaTextColor = Color.White;
                        PaymentCard.LandTextColor = Color.FromHex("#002447");

                    }

                });
            }
        }

        public ICommand PaymentCommand
        {
            get
            {
                return new Command(() =>
                {
                    

                });
            }
        }

        public EDeclarationPaymentViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
		}
	}
public enum PaymentTypes
    {
        Visa=1,
        SADAD=2
    }
}

