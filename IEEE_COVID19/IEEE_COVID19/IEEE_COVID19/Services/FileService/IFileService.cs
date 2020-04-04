using System.IO;

namespace IEEE_COVID19.Services.FileService
{
    public interface IFileService
    {
        string SaveFile(string name, Stream data, string location = "temp");
        bool DeleteFile(string fileName);
    }
}
