using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Windows.Input;

namespace Snake.Game.Helpers
{
    public class KeyboardConfigurationProvider : IKeyboardConfigurationProvider
    {
        private NameValueCollection _appsettings;

        private Dictionary<string, Key> _systemKeysMap;

        public KeyboardConfigurationProvider()
        {
            _appsettings = ConfigurationManager.AppSettings;
            GetSystemKeys();

            Up = _systemKeysMap[_appsettings["Up"]];
            Down = _systemKeysMap[_appsettings["Down"]];
            Left = _systemKeysMap[_appsettings["Left"]];
            Right = _systemKeysMap[_appsettings["Right"]];
        }

        public Key Up { get; private set; }
        
        public Key Down { get; private set; }

        public Key Left { get; private set; }

        public Key Right { get; private set; }

        private void GetSystemKeys()
        {
            _systemKeysMap = Enum.GetValues(typeof(Key)).Cast<Key>()
                .Select(x => new KeyboardKey { Name = x.ToString(), Key = (Key)x })
                .GroupBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(x => x.Key, x => x.First().Key);
        }
    }

    internal class KeyboardKey
    {
        public string Name {get; set;}
        public Key Key {get; set;}
    }
}
