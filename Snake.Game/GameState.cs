using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Game
{
    public static class GameState
    {
        public static int Score { get; set; } = 0;

        public static bool IsGameOver { get; set; }

        public static SnakeDirection SnakeDirection { get; set; }
    }
}
