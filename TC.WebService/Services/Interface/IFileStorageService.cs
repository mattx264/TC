using System.IO;
using System.Threading.Tasks;

namespace TC.WebService.Services.Interface
{
    public interface IFileStorageService
    {
        Task<string> StoreFileAsync(string filename, byte[] image);
        Task<string> StoreFileAsync(string filename, Stream image);
    }
}
