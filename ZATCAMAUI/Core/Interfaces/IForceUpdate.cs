namespace ZATCAMAUI.Core.Interfaces
{
    public interface IForceUpdate
    {
        Task FetchAndActivateAsync();
        string GetValue(string key);

    }
}

