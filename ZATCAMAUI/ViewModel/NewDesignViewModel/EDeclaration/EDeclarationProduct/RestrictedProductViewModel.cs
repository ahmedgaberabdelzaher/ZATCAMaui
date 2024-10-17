using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EDeclerationsModel;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationProduct
{
    public partial class BaseProductDeclarationViewModel
    {
        #region Properties

        string restrictedItem;
        public string RestrictedItem { get { return restrictedItem; } set { restrictedItem = value; OnPropertyChanged(); } }

        bool isPermit;
        public bool IsPermit { get { return isPermit; } set { isPermit = value; OnPropertyChanged(); } }

        ObservableCollection<ReportFileModel> restrictedUploadedFiles = new ObservableCollection<ReportFileModel>();
        public ObservableCollection<ReportFileModel> RestrictedUploadedFiles { get { return restrictedUploadedFiles; } set { restrictedUploadedFiles = value; OnPropertyChanged(); } }
        #endregion

        #region Commands

        public ICommand UploadFileCommand
        {
            get
            {
                return new Command(async () =>
                {
                    RestrictedUploadedFiles = await PickAndShow(new PickOptions() { PickerTitle = "Pick Files" }, RestrictedUploadedFiles, AppResources.PDFFileHintTwo, AppResources.NumberofAttachments, maxFileSize: 1);
                });
            }
        }
        public ICommand DeleteRestrictedAttatchementCommand
        {
            get
            {
                return new Command<ReportFileModel>((file) =>
                {

                    if (file != null && RestrictedUploadedFiles != null && RestrictedUploadedFiles.Count > 0)
                    {
                        RestrictedUploadedFiles.Remove(file);

                    }
                });
            }
        }
        public ICommand SelectIsPermitCommand
        {
            get
            {

                return new Command<string>((e) =>
                {
                    try
                    {
                        IsPermit = e == "0" ? false : true;
                    }
                    catch (Exception)
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

        public ICommand GoToProhibitedGoodsLstURlCommand
        {

            get
            {
                return new Command(async() =>
                {
                    try
                    {
                        var url = string.Empty;
                        if (App.IsArabic)
                        {
                            url = "https://zatca.gov.sa/ar/RulesRegulations/Taxes/Pages/customs_individual/Prohibited-goods.aspx";
                        }
                        else
                        {
                            url = "https://zatca.gov.sa/en/RulesRegulations/Taxes/Pages/customs_individual/Prohibited-goods.aspx";

                        }

                        await Launcher.OpenAsync(new Uri(url));
                    }
                    catch (Exception)
                    {

                    }

                });
            }
        }
        #endregion

        #region Methods
        private void AddRestricted()
        {
            if (CheckrestrictedDataNotNull())
            {

                if (int.Parse(Quantity ?? "0") <= 0)
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.QuantityValidation;
                    return;
                }
                var item = new Models.EDeclerationsModel.SubmitModels.Restricted()
                {
                    otherpurpose = OtherPurpose,
                    typeName = RestrictedItem,
                    unit = SelectedUnit.id,
                    count = int.Parse(Quantity ?? "0"),
                    value = double.Parse(TotalValue),
                    purpose = SelectedPurposes.ID,
                    currencyName = SelectedCurrencie.Name,
                    currency = SelectedCurrencie.currencyCode,
                    permit = IsPermit,
                    attachment = RestrictedUploadedFiles != null && RestrictedUploadedFiles.Count > 0 ? RestrictedUploadedFiles[0].fileBase64 : ""
                };
                var cardItem = new EDeclerationCardModel()
                {
                    Name = item.typeName,
                    desc = SelectedPurposes.Name,
                    Price = $"{TotalValue} {item.currencyName}",
                    QTY =Quantity,
                    ID = item.ID,
                    Type = 4
                };
                CardData.Add(cardItem);
                SubmitModel.travelerDeclaration.restricted.Add(item);
                ClearRestrictedData();
            }
        }

        private void ClearRestrictedData()
        {
            SelectedCurrencie = null;
            RestrictedItem = string.Empty;
            SelectedMaterialTypes = null;
            Quantity = null;
            OtherPurpose = "";
            IsPermit = false;
            SelectedUnit = null;
            RestrictedUploadedFiles = new ObservableCollection<ReportFileModel>();
            SelectedPurposes = null;
            TotalValue = null;
        }

        private bool CheckrestrictedDataNotNull()
        {
            if (IsPermit && RestrictedUploadedFiles.Count == 0)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.PleaseAttach;
                return false;
            }
            // Make sure items don't exceed 5
            else if (SubmitModel.travelerDeclaration.restricted.Count == 5)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RestrictedDisc;
                return false;
            }

            else if (SelectedCurrencie != null && SelectedPurposes != null && !string.IsNullOrWhiteSpace(TotalValue) && SelectedUnit != null && !string.IsNullOrWhiteSpace(Quantity) && !string.IsNullOrWhiteSpace(RestrictedItem))
            {
                if (SelectedPurposes.ID == 8)
                {
                    if (string.IsNullOrWhiteSpace(OtherPurpose))
                    {
                        DisplayRequiredDataMsg();
                        return false;

                    }
                }
                
            }
            
            return true;
        }
        #endregion
    }
}

