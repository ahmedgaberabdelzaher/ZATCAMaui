using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatExemptionRequestViewModel;
using ZATCAMAUI.Views.NewDesign.GenericPickers;

namespace ZATCAMAUI.Views.NewDesign.ZakatExemptionRequest;

public partial class ZakatExemptionPageView : ContentPage
{
    ZakatExemptionPageViewModel viewModel;
    ZakatExemptionModel _zakatExemptionModel;
    public ZakatExemptionPageView(ZakatExemptionModel zakatExemptionModel)
    {
        InitializeComponent();
        _zakatExemptionModel = zakatExemptionModel;

        viewModel = App.Locator.ZakatExemptionPageView;
        this.BindingContext = viewModel;

        SetViewsToDefault();
        LoadData();
        if (_zakatExemptionModel != null && _zakatExemptionModel.d != null && _zakatExemptionModel.d.Fbguid != null)
        {
            viewModel.SetSummaryDetails(_zakatExemptionModel);//opening page on edit mode to add additional details
            viewModel.ZakatExcemptionTPDetailsVisible = false;
        }
        else
        {
            Task.Run(() => this.viewModel.GetDetailsForZakatExeRwqAsync()).Wait();
        }

        InitializationPopups();

    }
    public async void LoadData()
    {
        try
        {
            viewModel.IsLoading = true;
            _zakatExemptionModel = await this.viewModel.GetDetailsForZakatExeRwq();//await Manager.ZakatExemptionWebServiceManager.GetRequestToZakatExemtionRequest(null);
            
            viewModel.IsLoading = false;
            if (_zakatExemptionModel?.data != null)
            {
                _zakatExemptionModel.d = _zakatExemptionModel.data;
                if (string.IsNullOrEmpty(_zakatExemptionModel.d.TinName))
                {
                    TPNAME.Text = " -- ";
                }
                else
                {
                    TPNAME.Text = _zakatExemptionModel.d.TinName;
                }

                if (string.IsNullOrEmpty(_zakatExemptionModel.d.Actnm))
                {
                    TPACTY.Text = "";
                }
                else
                {
                    TPACTY.Text = _zakatExemptionModel.d.Actnm;
                }



                if (string.IsNullOrEmpty(_zakatExemptionModel.d.Tin))
                {
                    TPTIN.Text = "";
                }
                else
                {
                    TPTIN.Text = _zakatExemptionModel.d.Tin;
                }

                if (string.IsNullOrEmpty(_zakatExemptionModel.d.Mobile))
                {
                    TPMN.Text = "";
                }
                else
                {
                    TPMN.Text = _zakatExemptionModel.d.Mobile;
                }

                if (string.IsNullOrEmpty(_zakatExemptionModel.d.Email))
                {
                    TPEA.Text = " -- ";
                }
                else
                {
                    TPEA.Text = _zakatExemptionModel.d.Email;
                }

                if (string.IsNullOrEmpty(_zakatExemptionModel.d.CrNumber))
                {
                    TPCN.Text = " -- ";
                }
                else
                {
                    TPCN.Text = _zakatExemptionModel.d.CrNumber;
                }

                if (string.IsNullOrEmpty(_zakatExemptionModel.d.CommDate))
                {
                    TPCNDATE.Text = " -- ";
                }
                else
                {
                    TPCNDATE.Text = _zakatExemptionModel.d.CommDate.ToString();
                }
            }
        }
        catch (Exception)
        {
            viewModel.IsLoading = false;
        }



    }

    private void SetViewsToDefault()
    {
        viewModel.SelectedYear = string.Empty;
        viewModel.SelectedYearID = string.Empty;
        viewModel.ZakatExemptionYearConButtonEnabled = false;

        viewModel.SelectedEntityType = string.Empty;
        viewModel.SelectedEntityID = string.Empty;
        viewModel.CompanyEstablishmentOther = string.Empty;
        viewModel.IsCharitabletrustsAndFoundations = false;
        viewModel.NatureOfEntity = string.Empty;
        viewModel.OffSpringPercentage = 0;
        viewModel.CharityPercentage = 0;
        viewModel.ZakatEntityInfoConButtonEnabled = false;
        viewModel.ZakatAttachmentConButtonEnabled = false;
        viewModel.ZakatJustificationConButtonEnabled = false;
        viewModel.IsInstrunctionChecked = false;

        viewModel.CompanyArticalsAttachmentsListViewData = new System.Collections.ObjectModel.ObservableCollection<Attachment>();
        viewModel.CertificateOfRegAttachmentsListViewData = new System.Collections.ObjectModel.ObservableCollection<Attachment>();
        viewModel.CharitableTrustAttachmentsListViewData = new System.Collections.ObjectModel.ObservableCollection<Attachment>();
        viewModel.CharityLicenseAttachmentsListViewData = new System.Collections.ObjectModel.ObservableCollection<Attachment>();
        viewModel.MemorandumOfAssociationAttachmentsListViewData = new System.Collections.ObjectModel.ObservableCollection<Attachment>();
        viewModel.OtherAttachmentsListViewData = new System.Collections.ObjectModel.ObservableCollection<Attachment>();

        viewModel.AdditionalAttachmentsListViewData = new System.Collections.ObjectModel.ObservableCollection<Attachment>();

        viewModel.JusticationOne = string.Empty;
        viewModel.JusticationTwo = string.Empty;
        viewModel.JusticationThree = string.Empty;
        viewModel.JusticationFour = string.Empty;
        viewModel.JusticationFive = string.Empty;

        viewModel.IsJustificationOneVisible = false;
        viewModel.IsJustificationTwoVisible = false;
        viewModel.IsJustificationThreeVisible = false;
        viewModel.IsJustificationFourVisible = false;

        viewModel.PageOnEditMode = false;
        viewModel.IsEditRequired = false;
        viewModel.ShowTermsOnSubmit = true;
        viewModel.CurrentIndex = 0;

        TPACTY.Text = " -- ";
        TPNAME.Text = " -- ";
        TPTIN.Text = " -- ";
        TPMN.Text = " -- ";
        TPEA.Text = " -- ";
        TPCNDATE.Text = " --";
        TPCN.Text = " -- ";


        viewModel.ZakatExcemptionTPDetailsVisible = true;
        viewModel.ZakatExemptionYearVisible = false;
        viewModel.EntityInformationVisible = false;
        viewModel.AttachmentInformationVisible = false;
        viewModel.JustificationInformationVisible = false;
        viewModel.SummaryVisible = false;
        viewModel.IsLoading = false;
    }

    private void InitializationPopups()
    {
        MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelected", (sender, arg) =>
        {
            // viewModel.PickerModelExcemptionYear = arg;

            var selectedType = string.Empty;
            string SelectedIDTypeValue = string.Empty;
            if (arg.PickerId == "ExemptionYearPicker")
            {
                viewModel.SelectedYear = arg.SelectedValue;
                viewModel.SelectedYearID = viewModel.GetYearIDByType(arg.SelectedValue);
                viewModel.ZakatExemptionYearConButtonEnabled = true;
            }
            else if (arg.PickerId == "EntityTypePicker")
            {
                viewModel.SelectedEntityType = arg.SelectedValue;
                viewModel.SelectedEntityID = viewModel.GetEntityIDByType(arg.SelectedValue);
                if (string.Equals(viewModel.SelectedEntityID, "03"))
                {
                    viewModel.IsCharitabletrustsAndFoundations = true;
                }
                else
                {
                    viewModel.IsCharitabletrustsAndFoundations = false;
                    viewModel.CompanyEstablishmentOther = string.Empty;
                }
            }
            else if (arg.PickerId == "CompanyEstablishmentOther")
            {
                viewModel.CompanyEstablishmentOther = arg.SelectedValue;
                viewModel.ZakatEntityInfoConButtonEnabled = true;

            }
            else if (arg.PickerId == "EntityCategory")
            {
                viewModel.SelectedEntityIDCategory = arg.SelectedValue;
            }

        });

        MessagingCenter.Subscribe<object, AttachmentsList>(this, "AttachmentReceived", (sender, arg) =>
        {
            if (arg != null)
            {
                viewModel.PopulateAttachments(arg.results);
            }
        });


    }



    private void NatureOfEntity_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (viewModel.NatureOfEntity.Length > 30)//If it is more than your character restriction
        {
            viewModel.NatureOfEntity = viewModel.NatureOfEntity.Remove(viewModel.NatureOfEntity.Length - 1);// Remove Last character 

        }

        if (viewModel.NatureOfEntity.Length > 0 && viewModel.SelectedEntityType.Length > 0 && viewModel.SelectedEntityIDCategory.Length > 0)
        {
            viewModel.ZakatEntityInfoConButtonEnabled = true;
        }
        else
        {
            viewModel.ZakatEntityInfoConButtonEnabled = false;
        }


    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
        //  MessagingCenter.Unsubscribe<>(this, "ZAKATDetails");


    }

    private void EntityInfoConBtn_Clicked(object sender, EventArgs e)
    {
        viewModel.EntityInfoToBeExemptedConBtnClicked();
        showAttachments();
    }

    private void showAttachments()
    {
        layout_attachment.Children.Clear();
        viewModel.AttachmentsUploaded = new Dictionary<string, bool>();
        viewModel.AttachmentsUploaded.Clear();
        if (viewModel.FilteredAttachments != null)
        {
            foreach (var attachment in viewModel.FilteredAttachments)
            {
                AttachmentView attachmentView = new AttachmentView(attachment.name, attachment.documentCategory, updateAttachment);
                layout_attachment.Children.Add(attachmentView);
                viewModel.AttachmentsUploaded.Add(attachment.documentCategory, false);
            }
        }
    }

    public void updateAttachment(string category, bool uploaded)
    {
        viewModel.AttachmentsUploaded[category] = uploaded;
        validateAttachments();
    }

    private bool validateAttachments()
    {
        bool isUploaded = false;
        foreach (var item in viewModel.AttachmentsUploaded.Values)
        {
            if (item)
            {
                isUploaded = true;
            }
            else
            {
                isUploaded = false;
            }
        }
        if (isUploaded)
        {
            viewModel.ZakatAttachmentConButtonEnabled = true;
        }
        else
        {
            viewModel.ZakatAttachmentConButtonEnabled = false;
        }
        return isUploaded;
    }
}