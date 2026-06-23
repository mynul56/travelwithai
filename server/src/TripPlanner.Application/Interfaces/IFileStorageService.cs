using System.Threading.Tasks;

namespace TripPlanner.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadPdfAsync(byte[] pdfBytes, string fileName);
    }
}
