using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EGAZT.Controls;
using EGAZT.Models.EDeclerationsModel;
using EGAZT.Models.SubmitReportModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationProduct
{
	public partial class BaseProductDeclarationViewModel
    {
        #region Properties

        bool isCurrencySelected = false;
        static ObservableCollection<CurrencyModel> Currencies;
        CurrencyModel selectedCurrencie;
        public CurrencyModel SelectedCurrencie { get { return selectedCurrencie; } set { selectedCurrencie = value; RaisePropertyChanged(); } }

        bool isCurrencyPermit;
        public bool IsCurrencyPermit { get { return isCurrencyPermit; } set { isCurrencyPermit = value; RaisePropertyChanged(); } }

        ObservableCollection<ReportFileModel> currencyUploadedFiles = new ObservableCollection<ReportFileModel>();
        public ObservableCollection<ReportFileModel> CurrencyUploadedFiles { get { return currencyUploadedFiles; } set { currencyUploadedFiles = value; RaisePropertyChanged(); } }

        #endregion

        #region Commands
        public ICommand SelectIsCurrencyPermitCommand
        {
            get
            {

                return new Command<string>((e) =>
                {
                    try
                    {
                        IsCurrencyPermit = e == "0" ? false : true;
                    }
                    catch (Exception ex)
                    {
                        IsLoading = false;
                    }
                    finally
                    {
                        IsLoading = false;
                    }

                });

            }
        }
        public ICommand DeleteAttatchementCommand
        {
            get
            {
                return new Command<ReportFileModel>((file) =>
                {

                    if (file != null && CurrencyUploadedFiles != null && CurrencyUploadedFiles.Count > 0)
                    {
                        CurrencyUploadedFiles.Remove(file);

                    }
                });
            }
        }

        public ICommand CurrencyUploadFileCommand
        {
            get
            {
                return new Command(async () =>
                {
                    CurrencyUploadedFiles = await PickAndShow(new PickOptions() { PickerTitle = "Pick Files" }, CurrencyUploadedFiles,AppResources.PDFFileHintTwo, AppResources.NumberofAttachments, maxFileSize: 1);
                });
            }
        }

        public ICommand OpenCurrenciesCommand
        {
            get
            {

                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        isCurrencySelected = true;
                        isPurposeSelected = false;
                        isMaterialTypeSelected = false;
                        isProductSubTypeSelected = false;
                        isProductTypeSelected = false;
                        isTobacoTypeSelected = false;
                        isTobacotemSelected = false;
                        isUnitsSelected = false;
                        if (Currencies == null || Currencies.Count > 0)
                        {
                            var currencies = await DeclerationServices.GetCurrencies();
                            Currencies = currencies?.Item1.data;
                        }

                        var result = Currencies.Select(c => new BottomSheetModel() { Id = c.currencyCode.ToString(), Name = c.Name });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.Purpose;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                        IsLoading = false;
                    }
                    catch (Exception ex)
                    {
                        IsLoading = false;
                    }
                    finally
                    {
                        IsLoading = false;
                    }

                });

            }
        }


        #endregion

        #region Methods
        private bool CheckCurrencyDataNotNull()
        {
            if (SelectedCurrencie != null && SelectedMaterialTypes != null && SelectedPurposes != null && !string.IsNullOrWhiteSpace(TotalValue))
            {
                if (SelectedPurposes.ID == 8)
                {
                    if (string.IsNullOrWhiteSpace(OtherPurpose))
                        return false;
                }
                return true;
            }
            return false;
        }

        private void AddCurrency()
        {
            if (CheckCurrencyDataNotNull())
            {


                var item = new Models.EDeclerationsModel.SubmitModels.Currency()
                {
                    otherpurpose = OtherPurpose,
                    typeName = SelectedMaterialTypes.Name,
                    typeID = SelectedMaterialTypes.ID,
                    purpose = SelectedPurposes.ID,
                    currencyName = SelectedCurrencie.Name,
                    currency = SelectedCurrencie.currencyCode,
                    permit = IsCurrencyPermit,
                    attachment = CurrencyUploadedFiles != null && CurrencyUploadedFiles.Count > 0 ? CurrencyUploadedFiles[0].fileBase64 : ""
                };
                SubmitModel.travelerDeclaration.currency.Add(item);
                var cardItem = new EDeclerationCardModel()
                {
                    Name = SelectedMaterialTypes.Name,
                    desc = SelectedPurposes.Name,
                    Price = TotalValue.ToString(),
                    ID = item.ID,
                    Type = 3
                };
                CardData.Add(cardItem);

                ClearCurrencyData();
            }
            else
            {
                DisplayRequiredDataMsg();
            }
        }

        private void ClearCurrencyData()
        {
            SelectedCurrencie = null;
            SelectedMaterialTypes = null;
            Quantity = null;
            OtherPurpose = "";
            SelectedPurposes = null;
            TotalValue = null;
            IsCurrencyPermit = false;
            CurrencyUploadedFiles = new ObservableCollection<ReportFileModel>();
        }
        #endregion
    }
}

