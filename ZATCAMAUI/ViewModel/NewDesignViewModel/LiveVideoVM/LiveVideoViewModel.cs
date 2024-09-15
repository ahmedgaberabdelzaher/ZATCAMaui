using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.LiveVideoVM
{
    public class LiveVideoViewModel : BaseViewModel
    {
        #region Properties
        bool showWebView = true;
        public bool ShowWebView { get { return showWebView; } set { showWebView = value; OnPropertyChanged(); } }


        string selectedVideo;
        public string SelectedVideo { get { return selectedVideo; } set { selectedVideo = value; OnPropertyChanged(); } }

        string videoUrl;
        public string VideoUrl { get { return videoUrl; } set { videoUrl = value; OnPropertyChanged(); } }

        ObservableCollection<BottomSheetModel> bottomSheetList = new ObservableCollection<BottomSheetModel>();
        public ObservableCollection<BottomSheetModel> BottomSheetList { get { return bottomSheetList; } set { bottomSheetList = value; OnPropertyChanged(); } }

        string selectedPortName;
        public string SelectedPortName { get { return selectedPortName; } set { selectedPortName = value; OnPropertyChanged(); } }

        string headerTitle = AppResources.LiveVideoTitle;
        public string HeaderTitle { get { return headerTitle; } set { headerTitle = value; OnPropertyChanged(); } }

        public ObservableCollection<BottomSheetModel> TempBottomSheetList { get; set; } = new ObservableCollection<BottomSheetModel>();

        ObservableCollection<VideoModel> liveVideosList = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> LiveVideosList { get { return liveVideosList; } set { liveVideosList = value; OnPropertyChanged(); } }

        string searchText;
        public string SearchText { get { return searchText; } set { searchText = value; OnPropertyChanged(); } }

        string liveVideoSubTitle = AppResources.PortLiveVideoSubTitle2;
        public string LiveVideoSubTitle { get { return liveVideoSubTitle; } set { liveVideoSubTitle = value; OnPropertyChanged(); } }


        #endregion

        public LiveVideoViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }

        #region Commands

        public ICommand SearchEntryCommand
        {

            get
            {
                return new Command<object>((e) =>
                {
                    try
                    {
                        if (e != null)
                        {
                            var entry = e as GAZTBorderlessEntry;
                            var value = entry.Text.ToLower();
                            if (string.IsNullOrWhiteSpace(value))
                                BottomSheetList = new ObservableCollection<BottomSheetModel>(TempBottomSheetList);
                            else
                            {
                                var result = TempBottomSheetList.Where(s => s.Name.ToLower().Contains(value));
                                BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }

                });
            }
        }

        public ICommand OpenPortCommand
        {
            get
            {
                return new Command(_ =>
                {
                    try
                    {


                        BottomSheetList = new ObservableCollection<BottomSheetModel>
                        {
                            new BottomSheetModel
                            {
                                Id = "0",
                                Name= AppResources.Salwa
                            },
                             new BottomSheetModel
                            {
                                Id = "1",
                                Name= AppResources.Aarar
                            },
                            new BottomSheetModel
                            {
                                Id = "2",
                                Name= AppResources.Alhaditha
                            },
                            new BottomSheetModel
                            {
                                Id = "3",
                                Name= AppResources.HalatAmmar
                            },
                            new BottomSheetModel
                            {
                                Id = "4",
                                Name= AppResources.Khafji
                            },
                            new BottomSheetModel
                            {
                                Id = "5",
                                Name= AppResources.Alwadeeaa
                            }
                        };
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.Port;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                    }
                    catch (Exception)
                    {
                    }

                });
            }
        }

        public ICommand OnAppearingCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        MainThread.BeginInvokeOnMainThread( () =>
                        {
                            HeaderTitle = AppResources.LiveVideoTitle;
                            LiveVideoSubTitle = AppResources.PortLiveVideoSubTitle2;
                            LiveVideosList = new ObservableCollection<VideoModel>();
                            SelectedPortName = string.Empty;
                            SelectedVideo = string.Empty;
                            VideoUrl = string.Empty;
                            ShowWebView = false;



                        });

                    }
                    catch (Exception)
                    {

                    }


                });
            }
        }
        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        if (IsShowBottomSheet)
                            IsShowBottomSheet = false;
                        HeaderTitle = AppResources.LiveVideoTitle;

                    }
                    catch (Exception)
                    {

                    }


                });
            }
        }


        public ICommand SelectedBottomItemCommand
        {
            get
            {
                return new Command<BottomSheetModel>(async (e) =>
                {
                    try
                    {
                        SelectedPortName = e.Name;
                        HeaderTitle = AppResources.LiveVideoTitle;
                        await SetListOfLivePorts(int.Parse(e.Id));

                        IsShowBottomSheet = false;
                        SearchText = string.Empty;
                    }
                    catch (Exception)
                    {
                    }


                });
            }
        }

        public ICommand SelectedVideoItemCommand
        {
            get
            {
                return new Command<VideoModel>((videoItem) =>
                {
                    try
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            IsLoading = true;

                            if (App.IsArabic)
                                LiveVideoSubTitle = $"{AppResources.PortLiveVideoSubTitle}{SelectedPortName}";
                            else
                                LiveVideoSubTitle = $"{AppResources.PortLiveVideoSubTitle} {SelectedPortName}";
                            SelectedVideo = videoItem.VideoName;
                            await Task.Delay(2000);
                            VideoUrl = videoItem.VideoURl;
                            foreach (var video in LiveVideosList)
                            {
                                video.IsSelected = video.VideoName != videoItem.VideoName ? false : true;
                            }
                            IsLoading = false;
                        });
                    }
                    catch (Exception)
                    {

                    }


                });
            }
        }


        #endregion

        #region Methods
        private async Task SetListOfLivePorts(int livePortsEnum)
        {
            IsLoading = true;
            await Task.Delay(2000);
            switch (livePortsEnum)
            {
                case (int)LivePortsEnum.Salwa:
                    LiveVideosList = new ObservableCollection<VideoModel>()
                    {
                         new VideoModel
                        {
                            Row =0,
                            Column =0,
                            IsSelected = true,
                            VideoName = AppResources.Port4Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=634406740f89c"

                        },
                        new VideoModel
                        {
                            Row =0,
                            Column =1,
                            IsSelected = false,
                            VideoName = AppResources.Port2Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=6343ec678a4de"


                        },
                        new VideoModel
                        {
                            Row =1,
                            Column =0,
                            IsSelected = false,
                            VideoName = AppResources.Port3Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=6343eb6725928"

                        },
                        new VideoModel
                        {
                            Row =1,
                            Column =1,
                            IsSelected = false,
                            VideoName = AppResources.Port1Name ,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=6343ed0b7e602"
                        }

                    };
                    break;

                case (int)LivePortsEnum.Aarar:
                    LiveVideosList = new ObservableCollection<VideoModel>()
                    {
                         new VideoModel
                        {
                            Row =0,
                            Column =0,
                            IsSelected = true,
                            VideoName = AppResources.Port4Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66cf572556781"

                        },
                        new VideoModel
                        {
                            Row =0,
                            Column =1,
                            IsSelected = false,
                            VideoName = AppResources.Port2Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d036758503c"


                        },
                        new VideoModel
                        {
                            Row =1,
                            Column =0,
                            IsSelected = false,
                            VideoName = AppResources.Port3Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d033a06d1c6"

                        },
                        new VideoModel
                        {
                            Row =1,
                            Column =1,
                            IsSelected = false,
                            VideoName = AppResources.Port1Name ,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d033fb1a99c"
                        }

                    };
                    break;

                case (int)LivePortsEnum.Alhaditha:
                    LiveVideosList = new ObservableCollection<VideoModel>()
                    {
                         new VideoModel
                        {
                            Row =0,
                            Column =0,
                            IsSelected = true,
                            VideoName = AppResources.Port4Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66e19f18f3690"

                        },
                        new VideoModel
                        {
                            Row =0,
                            Column =1,
                            IsSelected = false,
                            VideoName = AppResources.Port2Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66e19f18f3692"


                        },
                        new VideoModel
                        {
                            Row =1,
                            Column =0,
                            IsSelected = false,
                            VideoName = AppResources.Port3Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66e19f18f3691"

                        },
                        new VideoModel
                        {
                            Row =1,
                            Column =1,
                            IsSelected = false,
                            VideoName = AppResources.Port1Name ,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66e19f18f3693"
                        }

                    };
                    break;

                case (int)LivePortsEnum.HalatAmmar:
                    LiveVideosList = new ObservableCollection<VideoModel>()
                    {
                         new VideoModel
                        {
                            Row =0,
                            Column =0,
                            IsSelected = true,
                            VideoName = AppResources.Port4Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d053a371076"

                        },
                        new VideoModel
                        {
                            Row =0,
                            Column =1,
                            IsSelected = false,
                            VideoName = AppResources.Port2Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d05449b7c56"


                        },
                        new VideoModel
                        {
                            Row =1,
                            Column =0,
                            IsSelected = false,
                            VideoName = AppResources.Port3Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d05449b7c57"

                        },
                        new VideoModel
                        {
                            Row =1,
                            Column =1,
                            IsSelected = false,
                            VideoName = AppResources.Port1Name ,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d05400eca74"
                        }

                    };
                    break;

                case (int)LivePortsEnum.Khafji:
                    LiveVideosList = new ObservableCollection<VideoModel>()
                    {
                         new VideoModel
                        {
                            Row =0,
                            Column =0,
                            IsSelected = true,
                            VideoName = AppResources.Port4Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d05449b7c03"

                        },
                        new VideoModel
                        {
                            Row =0,
                            Column =1,
                            IsSelected = false,
                            VideoName = AppResources.Port2Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d05449b7c02"


                        },
                        new VideoModel
                        {
                            Row =1,
                            Column =0,
                            IsSelected = false,
                            VideoName = AppResources.Port3Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d05449b7c04"

                        },
                        new VideoModel
                        {
                            Row =1,
                            Column =1,
                            IsSelected = false,
                            VideoName = AppResources.Port1Name ,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d05449b7c01"
                        }

                    };
                    break;

                case (int)LivePortsEnum.Alwadeeaa:
                    LiveVideosList = new ObservableCollection<VideoModel>()
                    {
                         new VideoModel
                        {
                            Row =0,
                            Column =0,
                            IsSelected = true,
                            VideoName = AppResources.Port4Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d05449b7c05"

                        },
                        new VideoModel
                        {
                            Row =0,
                            Column =1,
                            IsSelected = false,
                            VideoName = AppResources.Port2Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d05449b7c06"


                        },
                        new VideoModel
                        {
                            Row =1,
                            Column =0,
                            IsSelected = false,
                            VideoName = AppResources.Port3Name,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d05449b7c07"

                        },
                        new VideoModel
                        {
                            Row =1,
                            Column =1,
                            IsSelected = false,
                            VideoName = AppResources.Port1Name ,
                            VideoURl="https://g2.ipcamlive.com/player/player.php?alias=66d05449b7c08"
                        }

                    };
                    break;

                default:
                    break;
            }
            if (App.IsArabic)
                LiveVideoSubTitle = $"{AppResources.PortLiveVideoSubTitle}{SelectedPortName}";
            else
                LiveVideoSubTitle = $"{AppResources.PortLiveVideoSubTitle} {SelectedPortName}";
            SelectedVideo = AppResources.Port4Name;
            ShowWebView = true;
            VideoUrl = LiveVideosList[0].VideoURl;
            IsLoading = false;

        }
        #endregion
    }

    public enum LivePortsEnum
    {
        Salwa = 0,
        Aarar = 1,
        Alhaditha = 2,
        HalatAmmar = 3,
        Khafji = 4,
        Alwadeeaa = 5,
    }
    public class VideoModel : ObservableRecipient
    {
        public int Row { get; set; }
        public int Column { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }
        public string VideoName { get; set; }
        public string VideoURl { get; set; }
    }
}

