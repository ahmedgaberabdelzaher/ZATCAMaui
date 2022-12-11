using System;
using System.Windows.Input;
using Xamarin.Forms;
using EGAZT.Models.EDeclerationsModel;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations
{
	public partial class EDeclarationInformationsViewModel 
    {
        public ICommand IDSelectionCommand
        {
            get
            {
                return new Command<string>((e) =>
                {
                    try
                    {
                        if (IsYesSelected)
                        {
                            
                            IsYesSelected = false;

                            if (SubmitModel.travelerDeclaration.travelID.ToLower().StartsWith("1"))
                            {
                                // passenger.travelDocumentType =; 
                            }
                            else if (SubmitModel.travelerDeclaration.travelID.ToLower().StartsWith("2"))
                            {

                            }
                            else
                            {
                                // Passport
                            }
                        }
                        else
                        {
                            IsYesSelected = true;
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                });
            }
        }

        public ICommand OpenNationalityCommand
        {
            get
            {

                return new Command(async () =>
                {
                    IsLoading = true;
                    isNationalitySelected = true;
                    //var reportType = await this._submitReportServices.GetReportType();
                    //var result = reportType?.reportTaxTypeList?.Select(c => new BottomSheetModel() { Id = c.reportTaxTypeCode, Name = c.reportTaxTypeName }).ToList() ?? new List<BottomSheetModel>();
                    //BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                    IsShowBottomSheet = true;
                    HeaderTitle = AppResources.ESTNationalityLabel;
                    TempBottomSheetList = BottomSheetList;
                    IsLoading = false;
                });

            }
        }

        public ICommand GenderSelectionCommand
        {
            get
            {
                return new Command<string>((g) =>
                {
                    if (IsYesSelected)
                    {
                        IsYesSelected = false;
                        SubmitModel.travelerDeclaration.gender = int.Parse(g); // Male
                    }
                    else
                    {
                        IsYesSelected = true;
                        SubmitModel.travelerDeclaration.gender = int.Parse(g); // Female
                    }
                });
            }
        }

        public ICommand OpenItsSourceCommand
        {
            get
            {

                return new Command(async () =>
                {
                    IsLoading = true;
                    isItsSourceSelected = true;
                    //var reportType = await this._submitReportServices.GetReportType();
                    //var result = reportType?.reportTaxTypeList?.Select(c => new BottomSheetModel() { Id = c.reportTaxTypeCode, Name = c.reportTaxTypeName }).ToList() ?? new List<BottomSheetModel>();
                    //BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                    IsShowBottomSheet = true;
                    HeaderTitle = AppResources.ItsSource;
                    TempBottomSheetList = BottomSheetList;
                    IsLoading = false;
                });

            }
        }

        public ICommand GoToTripInfoCommand
        {
            get
            {
                return new Command(_ =>
                {
                    _navigationService.NavigateTo("TripInformationPage");
                });
            }
        }
    }
}

