namespace Snake.Game.Helpers
{
    public interface IGameConfigurationProvider
    {
        int SnakeSpeed { get; }

        public int GameObjectSizeX { get; }

        public int GameObjectSizeY { get; }
    }
}
