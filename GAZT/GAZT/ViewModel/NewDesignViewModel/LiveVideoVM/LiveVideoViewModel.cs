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
using MediaManager;

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
           
        }

        #region Commands

         public ICommand OnAppearingCommand
         {
                get
                {
                    return new Command<VideoModel>(async(videoItem) =>
                    {
                        IsLoading = true;

                        // await GetYoutubeLiveVideoURl(LiveVideosList[0].VideoURl);
                        //await CrossMediaManager.Current.Play("https://www.youtube.com/watch?v=edkTRhGMGbc");
                        IsLoading = false;
                   
                    });
                }
         }
         public ICommand OnDisappearingCommand
         {
            get
            {
                return new Command<VideoModel>(async (videoItem) =>
                {
                    IsLoading = true;

                    await CrossMediaManager.Current.Stop();

                    IsLoading = false;

                });
            }
         }

        public ICommand SelectedVideoItemCommand
        {
                get
                {
                    return new Command<VideoModel>(async(videoItem) =>
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
        }


        #endregion

        #region Methods
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

