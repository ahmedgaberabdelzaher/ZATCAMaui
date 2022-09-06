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
using System.Net.Http;
using System.Web;
using System.Net;
using Newtonsoft.Json.Linq;

namespace EGAZT.ViewModel.NewDesignViewModel.LiveVideoVM
{
    public class LiveVideoViewModel: BaseViewModel
    {
        #region Properties

        string selectedVideo = AppResources.Port1Name;
        public string SelectedVideo { get { return selectedVideo; } set { selectedVideo = value; RaisePropertyChanged(); } }

        YoutubeClient youtube;



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
                VideoURl="s7ye-npc7Io"
            },
            new VideoModel
            {
                Row =0,
                Column =1,
                IsSelected = false,
                VideoNumber = AppResources.Port2Name,
                  VideoURl="edkTRhGMGbc"
            },
            new VideoModel
            {
                Row =1,
                Column =0,
                IsSelected = false,
                VideoNumber = AppResources.Port3Name,
               VideoURl="s7ye-npc7Io"
            },
            new VideoModel
            {
                Row =1,
                Column =1,
                IsSelected = false,
                VideoNumber = AppResources.Port4Name,
                  VideoURl="edkTRhGMGbc"
            }
        };
        public ObservableCollection<VideoModel> LiveVideosList { get { return liveVideosList; } set { liveVideosList = value; RaisePropertyChanged(); } }



        #endregion

        public LiveVideoViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
           youtube = new YoutubeClient();
            GetYoutubeLiveVideoURl(LiveVideosList[0].VideoURl);
           
            
        }

        #region Commands
/*
        int currentTab = 3;
        public int CurrentTab { get { return currentTab; } set { currentTab = value; RaisePropertyChanged(); } }

        public ICommand ChangeCurrentTabCommand
        {
            get
            {
                return new Command<string>((tab) =>
                {
                    if (tab != currentTab.ToString())
                    {
                        if (tab == "1")
                        {

                            _navigationService.NavigateTo($"/{App.SFLoginPageView}", App.GAZTNewDesignDashBoardPageView);
                            return;
                        }
                        _navigationService.NavigateTo("/Home", tab);

                    }

                });
            }
        }
*/
        public ICommand SelectedVideoItemCommand
        {
            get
            {
                return new Command(async(item) =>
                {
                    IsLoading = true;
                  //  VideoUrl = null;
                    var videoItem = item as VideoModel;
                   // VideoUrl = videoItem.VideoURl;
                    SelectedVideo = videoItem.VideoNumber;

                    foreach (var video in LiveVideosList)
                    {
                        video.IsSelected = video.VideoNumber != videoItem.VideoNumber ? false : true;
                    }

                    await GetYoutubeLiveVideoURl(videoItem.VideoURl);
                    /*string videoId = "uXQ2-eUjRYs";
                    if (videoItem.VideoNumber.Contains("2")|| videoItem.VideoNumber.Contains("4"))
                    {
                        await GetYoutubeLiveVideoURl(videoId);
                    }
                    else
                    {
                        await GetYoutubeLiveVideoURl("S2USjH3wLyA");
                        /*StreamManifest streamManifest = await youtube.Videos.Streams.GetManifestAsync("https://www.youtube.com/watch?v=CvH5QXUWtiE");
                        IVideoStreamInfo streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
                        if (streamInfo != null)
                        {
                            VideoUrl = streamInfo.Url;
                        }
                    }*/
                    IsLoading = false;
                   
                });
            }
        }

        private async Task GetYoutubeLiveVideoURl(string videoId)
        {
            var streamManifests = await youtube.Videos.Streams.GetHttpLiveStreamUrlAsync(videoId);
            VideoUrl = streamManifests;
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

