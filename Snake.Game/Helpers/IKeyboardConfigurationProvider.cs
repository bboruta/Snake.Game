using Snake.Contract;
using Snake.Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Snake.Game.Helpers
{
    public interface IKeyboardConfigurationProvider
    {
        Key Up { get; }

        Key Down { get; }

        Key Left { get; }

        Key Right { get; }
    }
}
