namespace ZATCAMAUI.Core.Interfaces
{

    public interface IFileHelperService
    {
        MemoryStream GetFileStream();
        //Gets the file stream in UWP
        Task<MemoryStream> GetFileStreamAsync();
    }
}
