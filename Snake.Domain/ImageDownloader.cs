using Snake.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Domain
{
    public class ImageDownloader : IImageDownloader
    {
        private IWebImageRepository _webImageRepository;

        private Random _random = new Random();

        public ImageDownloader(IWebImageRepository webImageRepository)
        {
            _webImageRepository = webImageRepository;
        }

        public (byte[], int) DownloadRandomSnakeImageAsync(IEnumerable<string> imagesLinks, int lastImageIndex)
        {
            if (lastImageIndex == imagesLinks.Count() - 1)
            {
                lastImageIndex = _random.Next(0, lastImageIndex);
            }
            else if (lastImageIndex == 0)
            {
                lastImageIndex = _random.Next(1, imagesLinks.Count());
            }
            else
            {
                var potentialImageIndexA = _random.Next(0, lastImageIndex);
                var potentialImageIndexB = _random.Next(lastImageIndex + 1, imagesLinks.Count());
                lastImageIndex = _random.Next(2) == 1 ? potentialImageIndexA : potentialImageIndexB;
            }

            var imageLink = imagesLinks.ElementAt(lastImageIndex);
            var image = _webImageRepository.GetImageFromWeb(imageLink);

            return (image.Result, lastImageIndex);
        }
    }
}
