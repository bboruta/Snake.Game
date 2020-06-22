using Snake.Contract;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Snake.Infrastructure
{
    public class WebImageRepository : IWebImageRepository
    {
        public async Task<byte[]> GetImageFromWeb(string link)
        {
            var httpClient = new HttpClient();
            var buffer = await httpClient.GetByteArrayAsync(link);

            using (var stream = new MemoryStream(buffer))
            {
                buffer = stream.ToArray();
            }

            return buffer;
        }
    }
}
