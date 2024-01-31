using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.CustomServices;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class CommonViewModel : BaseViewModel
    {
        ICommonServices _commonServices;
        public CommonViewModel(INavigationService navigationService, IDialogService dialogService, ICommonServices commonServices) : base(navigationService, dialogService)
        {
            _commonServices = commonServices;
        }
        ObservableCollection<CustomPort> ports { get; set; }

        public ObservableCollection<CustomPort> Ports
        {
            get { return ports; }

            set
            {
                if (ports == value)
                {
                    return;
                }

                ports = value;
                RaisePropertyChanged();
            }
        }

        bool isShowMsgView { get; set; }

        public bool IsShowMsgView
        {
            get { return isShowMsgView; }

            set
            {
                isShowMsgView = value;
                RaisePropertyChanged();
            }
        }

        string messageTxt { get; set; }

        public string MessageTxt
        {
            get { return messageTxt; }

            set
            {
                messageTxt = value;
                RaisePropertyChanged();
            }
        }

        public ICommand CloseMsgViewCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsShowMsgView = false;
                });
            }
        }


        public static ObservableCollection<CustomPort> PortsStaticLst { get; set; }

        CustomPort selectedPort { get; set; } = null;

        public CustomPort SelectedPort
        {
            get { return selectedPort; }

            set
            {
                selectedPort = value;
                RaisePropertyChanged();
            }
        }

        public ICommand OpenPortsCommand
        {
            get
            {
                return new Command(async () =>
                {

                    if (PortsStaticLst != null && PortsStaticLst.Count > 0)
                    {
                        /*var res = await ActionSheet.ShowActionSheet(null, AppResources.CancelText, null, Ports.Select(c => c.Name).ToArray());
                        if (!String.IsNullOrEmpty(res) && res != AppResources.CancelText)
                        {
                            SelectedPort = Ports.First(c => c.Name == res);
                        }*/
                        Ports = PortsStaticLst;
                        IsPortsPickerSearch = true;
                        IsPickerOpened = true;
                    }
                    else
                    {
                        MessageTxt = AppResources.NoDataFound;
                        IsShowMsgView = true;
                    }
                });
            }
        }

        public async Task GetPorts()
        {
            try
            {
                IsLoading = true;
                var data = await _commonServices.GetCustomPorts();
                if (data.Item2)
                {
                    Ports = data.Item1.Data;
                    PortsStaticLst = Ports;

                }
            }
            catch (Exception)
            {

            }
            finally { IsLoading = false; }
        }


        bool isPickerOpened { get; set; }

        public bool IsPickerOpened
        {
            get { return isPickerOpened; }

            set
            {
                isPickerOpened = value;
                RaisePropertyChanged();
            }
        }

        bool isPortsPickerSearch { get; set; }

        public bool IsPortsPickerSearch
        {
            get { return isPortsPickerSearch; }

            set
            {
                isPortsPickerSearch = value;

                RaisePropertyChanged();
            }
        }

        string _SearchTxt;
        public string SearchTxt
        {
            get
            {
                return _SearchTxt;
            }
            set
            {
                if (_SearchTxt == value) return;

                _SearchTxt = value;
                RaisePropertyChanged();
            }
        }


        public ICommand PortSelectionChangedCommand
        {
            get
            {
                return new Command<object>((e) =>
                {
                    SelectedPort = e as CustomPort;
                    clear();

                });
            }
        }

        protected void clear()
        {
            Ports = PortsStaticLst;
            IsPortsPickerSearch = IsPickerOpened = false;
            SearchTxt = "";
        }

        public ICommand ClosePickerCommand
        {
            get
            {
                return new Command(() =>
                {

                    clear();
                });
            }
        }


        public ICommand SearchInPortsCommand
        {

            get
            {
                return new Command(() =>
                {
                    try
                    {

                        if (IsPickerOpened)
                        {
                            var res = PortsStaticLst.Where(c => c.Name.Contains(SearchTxt));
                            Ports = new ObservableCollection<CustomPort>(res);
                        }


                    }
                    catch (Exception)
                    {

                    }

                });
            }
        }

    }
}
