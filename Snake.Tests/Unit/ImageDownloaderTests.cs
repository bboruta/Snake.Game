using NSubstitute;
using NUnit.Framework;
using Snake.Contract;
using Snake.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snake.Tests.Unit
{
    [TestFixture]
    class ImageDownloaderTests
    {
        private ImageDownloader _uut;

        private IWebImageRepository _webImageRepository;
        private Random _random = new Random();

        [SetUp]
        public void SetUp()
        {
            _webImageRepository = Substitute.For<IWebImageRepository>();
            byte[] randomBytes = new byte[10];
            _random.NextBytes(randomBytes);
            _webImageRepository.GetImageFromWeb("").Returns(Task.FromResult(randomBytes));

            _uut = new ImageDownloader(_webImageRepository);
        }

        [Test]
        public void GivenTwoImagesWhenSecondImageWasPreviouslyChosenThenFirstOneShouldBeChosen()
        {
            // arrange
            IEnumerable<string> links = new List<string>()
            {
                "link1",
                "link2"
            };

            int lastImageIndex = 1;

            // act
            var (_, newIndex) = _uut.DownloadRandomSnakeImageAsync(links, lastImageIndex);

            // assert
            Assert.AreEqual(0, newIndex);
        }

        [Test]
        public void WhenThereIsOnlyOneImageThenJustReturnSameImage()
        {
            // arrange
            IEnumerable<string> links = new List<string>()
            {
                "link1"
            };

            int lastImageIndex = 0;

            // act
            var (_, newIndex) = _uut.DownloadRandomSnakeImageAsync(links, lastImageIndex);

            // assert
            Assert.AreEqual(0, newIndex);
        }

        [Test]
        public void WhenLastImageIndexIsBelowZeroThenThrowException()
        {
            // arrange
            IEnumerable<string> links = new List<string>()
            {
                "link1"
            };

            int lastImageIndex = -1;

            // act
            //var (_, newIndex) = _uut.DownloadRandomSnakeImageAsync(links, lastImageIndex);

            // act and assert
            Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>()
                .And.Message.EqualTo(
                "Specified argument was out of the range of valid values. (Parameter 'lastImageIndex')"),
                () => _uut.DownloadRandomSnakeImageAsync(links, lastImageIndex));
        }
    }
}
