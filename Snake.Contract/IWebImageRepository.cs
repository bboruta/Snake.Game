using System.Threading.Tasks;

namespace Snake.Contract
{
    public interface IWebImageRepository
    {
        Task<byte[]> GetImageFromWeb(string link);
    }
}
