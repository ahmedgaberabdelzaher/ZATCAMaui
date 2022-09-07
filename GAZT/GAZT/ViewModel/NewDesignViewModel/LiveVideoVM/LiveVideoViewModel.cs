using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Views;
using IDialogService = GalaSoft.MvvmLight.Views.IDialogService;
using System.Windows.Input;
using Xamarin.Forms;
using GalaSoft.MvvmLight;
using YoutubeExplode;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Library;

namespace EGAZT.ViewModel.NewDesignViewModel.LiveVideoVM
{
    public class LiveVideoViewModel: BaseViewModel
    {
        #region Properties

        string selectedVideo = AppResources.Port1Name;
        public string SelectedVideo { get { return selectedVideo; } set { selectedVideo = value; RaisePropertyChanged(); } }

        YoutubeClient youtube = new YoutubeClient();

        string videoUrl;
        public string VideoUrl { get { return videoUrl; } set { videoUrl = value; RaisePropertyChanged(); } }

        ObservableCollection<VideoModel> liveVideosList = new ObservableCollection<VideoModel>()
        {
            new VideoModel
            {
                Row =0,
                Column =0,
                IsSelected = true,
                VideoNumber = AppResources.Port1Name ,
                VideoURl="onc4gSz5XqY"

            },
            new VideoModel
            {
                Row =0,
                Column =1,
                IsSelected = false,
                VideoNumber = AppResources.Port2Name,
                VideoURl="ycF3wtfRpAM"

            },
            new VideoModel
            {
                Row =1,
                Column =0,
                IsSelected = false,
                VideoNumber = AppResources.Port3Name,
                VideoURl="Y2q0ELpgPYs"

            },
            new VideoModel
            {
                Row =1,
                Column =1,
                IsSelected = false,
                VideoNumber = AppResources.Port4Name,
                VideoURl="8eJJ6EAMoO8"

            }
        };
        public ObservableCollection<VideoModel> LiveVideosList { get { return liveVideosList; } set { liveVideosList = value; RaisePropertyChanged(); } }



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
                            Device.BeginInvokeOnMainThread(async() =>
                            {
                                IsLoading = true;
                                await GetYoutubeLiveVideoURl(LiveVideosList[0].VideoURl);
                                IsLoading = false;
                            });
                           
                        }
                        catch (Exception ex)
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
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            IsLoading = true;

                            SelectedVideo = videoItem.VideoNumber;

                            foreach (var video in LiveVideosList)
                            {
                                video.IsSelected = video.VideoNumber != videoItem.VideoNumber ? false : true;
                            }

                            await GetYoutubeLiveVideoURl(videoItem.VideoURl);
                            IsLoading = false;
                        });
                    }
                    catch (Exception ex)
                    {

                    }
                   
                   
                });
            }
        }


        #endregion

        #region Methods
        private async Task GetYoutubeLiveVideoURl(string videoId)
        {
            
            try
            {
                var streamManifests = await youtube.Videos.Streams.GetHttpLiveStreamUrlAsync(videoId);
                VideoUrl = streamManifests;

                if(Device.RuntimePlatform == Device.Android)
                {
                    var item = await CrossMediaManager.Current.Extractor.CreateMediaItem(VideoUrl);

                    item.MediaType = MediaType.Hls;
                }

            }
            catch (Exception ex)
            {

            }
         

        }
        #endregion
    }

    public class VideoModel : ViewModelBase
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
                RaisePropertyChanged();
            }
        }
        public string VideoNumber { get; set; }
        public string VideoURl { get; set; }
    }
}

