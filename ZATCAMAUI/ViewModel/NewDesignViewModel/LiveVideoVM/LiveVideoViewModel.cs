using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.LiveVideoVM
{
    public class LiveVideoViewModel : BaseViewModel
    {
        #region Properties

        string selectedVideo;
        public string SelectedVideo { get { return selectedVideo; } set { selectedVideo = value; OnPropertyChanged(); } }

        string videoUrl;
        public string VideoUrl { get { return videoUrl; } set { videoUrl = value; OnPropertyChanged(); } }

        ObservableCollection<VideoModel> liveVideosList = new ObservableCollection<VideoModel>();
        public ObservableCollection<VideoModel> LiveVideosList { get { return liveVideosList; } set { liveVideosList = value; OnPropertyChanged(); } }

        public void GetLiveVideoLst()
        {
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

        }


        #endregion

        public LiveVideoViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }

        #region Commands

        public ICommand OnAppearingCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                       MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            IsLoading = true;
                            GetLiveVideoLst();
                            await Task.Delay(2000);
                            SelectedVideo = AppResources.Port4Name;
                            VideoUrl = LiveVideosList[0].VideoURl;
                            IsLoading = false;
                        });

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

        #endregion
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

