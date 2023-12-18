namespace ZATCAMAUI.Core.Helper
{
    public static class ActionSheet
    {
        private static Page CurrentMainPage { get { return Application.Current.MainPage; } }

        public static async Task<string> ShowActionSheet(string title, string cancel, string destruction = null, string[] buttons = null)
        {
            var displayButtons = buttons ?? new string[] { };
            var action = await CurrentMainPage.DisplayActionSheet(title, cancel, destruction, displayButtons);
            return action;
        }
    }
}
