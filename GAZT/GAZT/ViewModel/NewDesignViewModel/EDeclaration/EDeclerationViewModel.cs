using System;
using System.Collections.Generic;
using System.Windows.Input;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using ZXing.Aztec.Internal;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
    public class EDeclerationViewModel: BaseEDeclarationViewModel
    {
        /// <summary>
        /// 1 for New Decleration
        /// 2 for Prevous Requests
        /// </summary>
        int serviceType;
        public int ServiceType { get { return serviceType; } set { serviceType = value; RaisePropertyChanged(); } }

        /// <summary>
        /// 1 for Visitor
        /// 2 for Citizen
        /// </summary>
        int identityType;
        public int IdentityType { get { return identityType; } set { identityType = value; RaisePropertyChanged(); } }


        public ICommand SelcectServiceTypeCommand
        {
            get
            {
                return new Command<string>((e) =>
                {
                    ServiceType =int.Parse(e);
                });
            }
        }

        public ICommand SelcectIdentityTypeCommand
        {
            get
            {
                return new Command<string>((e) =>
                {
                    IdentityType = int.Parse(e);
                    if (IdentityType==1)
                    {
                        _navigationService.NavigateTo("NewDeclarationPage","");
                    }
                    else
                    {
                        // _navigationService.NavigateTo("IAMLoginView", 1);
                        //  var payload= GetTokenData();
                       var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6InNhYmR1bG1vaXpAemF0Y2EuZ292LnNhIiwiRW1haWwiOiJzYWJkdWxtb2l6QHphdGNhLmdvdi5zYSIsIk1vYmlsZSI6IjUwOTMzOTM2NCIsIk5hdGlvbmxJZCI6IjEwMzExNjQ0NTAiLCJJZCI6IjIyODE3NDIiLCJGaXJzdE5hbWUiOiLYrdiz2KfZhSIsIk1pZGRsZU5hbWUiOiLYudmE2YoiLCJMYXN0TmFtZSI6Itin2YTYsdmB2KfYudmKIiwiTmF0aW9uYWxpdHlJZCI6IjEwMCIsIk5hdGlvbmFsaXR5Ijoi2KfZhNmF2YXZhNmD2Kkg2KfZhNi52LHYqNmK2Kkg2KfZhNiz2LnZiNiv2YrYqSIsIkdlbmRlciI6Ik1hbGUiLCJSZWxlYXNlRGF0ZSI6IjE0MzkvMDIvMjciLCJFbmREYXRlIjoiIiwiSXRzU291cmNlIjoiIiwiZXhwIjoxNjc5NjY1MDI4LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjYwNjA0IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo2MDYwNCJ9.QtFwlVBPRXnladbZ2OJeoz7Aewdl7-ZGmZ53dSHzRcg";

                        _navigationService.NavigateTo("NewDeclarationPage",token);

                    }
                });
            }
        }


        public EDeclerationViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices) : base(navigationService, dialogService,null)
        {
           /* if (IdentityType==2)
            {
                GetTokenData();
            }*/
        }

      
    }


}

