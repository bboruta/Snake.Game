using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Snake.Game.Helpers
{
    class GameConfigurationProvider : IGameConfigurationProvider
    {
        private NameValueCollection _appsettings;

        public GameConfigurationProvider()
        {
            _appsettings = ConfigurationManager.AppSettings;
        }

        public int SnakeSpeed => int.Parse(_appsettings["SnakeSpeed"]);

        public int GameObjectSizeX => int.Parse(_appsettings["GameObjectSizeX"]);

        public int GameObjectSizeY => int.Parse(_appsettings["GameObjectSizeY"]);
    }
}
