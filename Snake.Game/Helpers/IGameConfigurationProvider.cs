using System.Collections.Generic;
using System.Windows.Documents;

namespace Snake.Game.Helpers
{
    public interface IGameConfigurationProvider
    {
        int SnakeSpeed { get; }

        int GameObjectSizeX { get; }

        int GameObjectSizeY { get; }

        List<string> ImagesLinks { get; }
    }
}
