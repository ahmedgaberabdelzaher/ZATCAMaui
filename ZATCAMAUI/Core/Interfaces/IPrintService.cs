namespace ZATCAMAUI.Core.Interfaces
{
    public interface IPrintService
    {
        Task Save(MemoryStream stream, string fileName);
    }
}
