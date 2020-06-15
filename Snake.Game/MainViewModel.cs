using Microsoft.Extensions.Configuration;
using Snake.Contract;
using Snake.Game.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Game
{
    public class MainViewModel
    {
        private List<SnakePart> _snake = new List<SnakePart>();
        private Food _food = new Food();

        public MainViewModel(IKeyboardConfigurationProvider keyboardConfigurationProvider)
        {
            var a = keyboardConfigurationProvider;
        }
    }
}
