using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Contract
{
    public interface IImageDownloader
    {
        byte[] DrawSnakeImage(IEnumerable<string> imagesLinks);
    }
}
