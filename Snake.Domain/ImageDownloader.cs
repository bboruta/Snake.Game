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
            lastImageIndex = SelectNewRandomIndex(imagesLinks.Count(), lastImageIndex);
            var imageLink = imagesLinks.ElementAt(lastImageIndex);
            var image = _webImageRepository.GetImageFromWeb(imageLink);

            return (image.Result, lastImageIndex);
        }

        private int SelectNewRandomIndex(int imagesCount, int lastImageIndex)
        {
            if (lastImageIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lastImageIndex));
            }

            if (imagesCount.Equals(1))
            {
                return lastImageIndex;
            }

            if (lastImageIndex == imagesCount - 1)
            {
                lastImageIndex = _random.Next(0, lastImageIndex);
            }
            else if (lastImageIndex == 0)
            {
                lastImageIndex = _random.Next(1, imagesCount);
            }
            else
            {
                var potentialImageIndexA = _random.Next(0, lastImageIndex);
                var potentialImageIndexB = _random.Next(lastImageIndex + 1, imagesCount);
                lastImageIndex = _random.Next(2) == 1 ? potentialImageIndexA : potentialImageIndexB;
            }

            return lastImageIndex;
        }
    }
}
