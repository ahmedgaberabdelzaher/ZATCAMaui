using System;
using EGAZT.Controls;
using EGAZT.Models.SubmitReportModel;
using EGAZT.Services.Interface;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Services.Dialogs;
using GalaSoft.MvvmLight.Views;
using IDialogService = GalaSoft.MvvmLight.Views.IDialogService;
using System.Windows.Input;
using Xamarin.Forms;
using GalaSoft.MvvmLight;
using System.Linq;
using System.Collections;
using Xamarin.Forms.Internals;
using Xamarin.CommunityToolkit.UI.Views;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using Xamarin.CommunityToolkit.Core;
using System.Threading.Tasks;

namespace EGAZT.ViewModel.NewDesignViewModel.LiveVideoVM
{
    public class LiveVideoViewModel: BaseViewModel
    {
        #region Properties

        string selectedVideo = AppResources.PortName + "1";
        public string SelectedVideo { get { return selectedVideo; } set { selectedVideo = value; RaisePropertyChanged(); } }

        string videoUrl;
        public string VideoUrl { get { return videoUrl; } set { videoUrl = value; RaisePropertyChanged(); } }

        ObservableCollection<VideoModel> liveVideosList = new ObservableCollection<VideoModel>()
        {
            new VideoModel
            {
                Row =0,
                Column =0,
                IsSelected = true,
                VideoNumber = AppResources.PortName + "1"
            },
            new VideoModel
            {
                Row =0,
                Column =1,
                IsSelected = false,
                VideoNumber = AppResources.PortName + "2"
            },
            new VideoModel
            {
                Row =1,
                Column =0,
                IsSelected = false,
                VideoNumber = AppResources.PortName + "3"
            },
            new VideoModel
            {
                Row =1,
                Column =1,
                IsSelected = false,
                VideoNumber = AppResources.PortName + "4"
            }
        };
        public ObservableCollection<VideoModel> LiveVideosList { get { return liveVideosList; } set { liveVideosList = value; RaisePropertyChanged(); } }



        #endregion

        public LiveVideoViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }

        #region Commands

        public ICommand SelectedVideoItemCommand
        {
            get
            {
                return new Command(async(item) =>
                {
                    IsLoading = true;
                    VideoUrl = null;
                    var videoItem = item as VideoModel;

                    SelectedVideo = videoItem.VideoNumber;

                    foreach (var video in LiveVideosList)
                    {
                        video.IsSelected = video.VideoNumber != videoItem.VideoNumber ? false : true;
                    }

                    YoutubeClient youtube = new YoutubeClient();
                    if (videoItem.VideoNumber.Contains("2"))
                    {
                        var streamManifests = await youtube.Videos.Streams.GetHttpLiveStreamUrlAsync("4Q5CBT_E_k4");
                        VideoUrl = streamManifests;
                    }
                    else
                    {
                        
                        StreamManifest streamManifest = await youtube.Videos.Streams.GetManifestAsync("https://www.youtube.com/watch?v=CvH5QXUWtiE");
                        IVideoStreamInfo streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
                        if (streamInfo != null)
                        {
                            VideoUrl = streamInfo.Url;
                        }
                    }
                    IsLoading = false;
                   
                });
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
    }
}

