
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.Core.Helper
{

    public class DialogService : IDialogService
    {
        private Page _dialogPage;
        private FlowDirection flowDirection;
        public void Initialize(Page dialogPage)
        {
            _dialogPage = dialogPage;
            
        }
        #region IDialogService implementation
        public async Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            flowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            await _dialogPage.DisplayAlert(
                title,
                message,
                buttonText, flowDirection);
            if (afterHideCallback != null)
            {
                afterHideCallback();
            }
        }

        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            flowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            await _dialogPage.DisplayAlert(
                title,
                error.Message,
                buttonText, flowDirection);
            if (afterHideCallback != null)
            {
                afterHideCallback();
            }
        }

        public async Task ShowMessage(string message, string title)
        {
            flowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            await _dialogPage.DisplayAlert(
                title,
                message,
                AppResources.ZZZOkayText, flowDirection);
        }

        public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            flowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            await _dialogPage.DisplayAlert(
                title,
                message,
                buttonText, flowDirection);
            if (afterHideCallback != null)
            {
                afterHideCallback();
            }
        }
        public async Task<bool> ShowMessage(string title, string message, string accept, string cancel)
        {
            flowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            return await _dialogPage.DisplayAlert(
                title,
                message,
                accept, cancel ,flowDirection);
        }

        public async Task ShowMessage(string title, string message, string cancel)
        {
            flowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            await _dialogPage.DisplayAlert(
                title,
                message,
                cancel, flowDirection);
        }

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            flowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            var result = await _dialogPage.DisplayAlert(
                title,
                message,
                buttonConfirmText,
                buttonCancelText, flowDirection);
            if (afterHideCallback != null)
            {
                afterHideCallback(result);
            }
            return result;
        }

        public async Task ShowMessageBox(string message, string title)
        {
            flowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            await _dialogPage.DisplayAlert(
                title,
                message,
               AppResources.ZZZOkayText, flowDirection);
        }
        #endregion
    }
    
}
