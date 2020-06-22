using System.Collections.Generic;

namespace Snake.Contract
{
    public interface IImageDownloader
    {
        (byte[], int) DownloadRandomSnakeImageAsync(IEnumerable<string> imagesLinks, int lastImageIndex);
    }
}
