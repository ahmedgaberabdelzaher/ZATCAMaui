using System;
namespace ZATCAMAUI.Core.Interfaces
{
    public interface IDialogService
    {
        Task ShowError(string message, string title, string buttonText, Action afterHideCallback);
        Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback);
        Task<bool> ShowMessage(string title, string message, string accept, string cancel);
        Task ShowMessage(string title, string message, string cancel);
        Task ShowMessage(string message, string title);
        Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback);
        Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback);
        Task ShowMessageBox(string message, string title);
    }
}

