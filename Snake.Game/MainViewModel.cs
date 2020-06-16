using Microsoft.Extensions.Configuration;
using Snake.Contract;
using Snake.Game.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;

namespace Snake.Game
{
    public class MainViewModel : ViewModelBase
    {
        private IKeyboardConfigurationProvider _keyboardConfigurationProvider;
        private IGameConfigurationProvider _gameConfigurationProvider;

        private List<SnakePart> _snake = new List<SnakePart>();
        private Food _food;
        private DispatcherTimer timer = new DispatcherTimer();        
        private Random _random = new Random();

        private Key _up;
        private Key _down;
        private Key _left;
        private Key _right;

        private Dictionary<Key, SnakeDirection> _allowedMovementKeysMap;

        public MainViewModel(IKeyboardConfigurationProvider keyboardConfigurationProvider, IGameConfigurationProvider gameConfigurationProvider)
        {
            _keyboardConfigurationProvider = keyboardConfigurationProvider;
            _gameConfigurationProvider = gameConfigurationProvider;

            _up = _keyboardConfigurationProvider.Up;
            _down = _keyboardConfigurationProvider.Down;
            _left = _keyboardConfigurationProvider.Left;
            _right = _keyboardConfigurationProvider.Right;

            _allowedMovementKeysMap = new Dictionary<Key, SnakeDirection>()
            {
                {_up, SnakeDirection.Up },
                {_down, SnakeDirection.Down },
                {_left, SnakeDirection.Left },
                {_right, SnakeDirection.Right }
            };

            OnKeyDownCommand = new RelayCommandGeneric<Key>(p => OnKeyDown(p), p => true);


            timer.Interval = TimeSpan.FromMilliseconds(1000 / gameConfigurationProvider.SnakeSpeed);
            timer.Tick += UpdateScreen;
            timer.Start();

            StartGame();
        }

        public ICommand OnKeyDownCommand { get; }

        public int GameAreaDimensionX { get; set; } = 400;

        public int GameAreaDimensionY { get; set; } = 400;

        private void StartGame()
        {
            _snake.Clear();
            var head = new SnakePart(10, 5);
            _snake.Add(head);

            GenerateFood();
        }

        private void GenerateFood()
        {
            int maxPosX = GameAreaDimensionX / _gameConfigurationProvider.GameObjectSizeX;
            int maxPosY = GameAreaDimensionY / _gameConfigurationProvider.GameObjectSizeY;

            int foodPosX = _random.Next(0, maxPosX);
            int foodPosY = _random.Next(0, maxPosY);

            _food = new Food(foodPosX, foodPosY);
        }

        // to musi jakos odpowiadac na nacisniecie strzalki
        private void UpdateScreen(object sender, EventArgs e)
        {
            //sprawdzenie, czy przycisk przypisany do jakiegos ruchu zostal nacisniety
            //zrobic jakis detector przycisku bez switch case pozniej - jakies przypisanie eventu i klasa ktora przeslania odpowiednio
            // set snake direction
            // then update snake position
            //MoveSnake();

            var a = 5;

            //zmienic kierunek i ruszyc weza 
        }

        private void UpdateSnakeDirection(SnakeDirection direction)
        {
            //var allPossibleKeys = Enum.GetValues(typeof(Key));
            SnakeDirection = direction;

            //GetDirectionForPressedKey
        }

        
        private SnakeDirection GetDirectionForPressedKey(Key key)
        {
            if (_allowedMovementKeysMap.ContainsKey(key))
            {
                return _allowedMovementKeysMap[key];
            }

            return GameState.SnakeDirection;
        }

        private void OnKeyDown(Key pressedKey)
        {
            var direction = GetDirectionForPressedKey(pressedKey);
            UpdateSnakeDirection(direction);
        }
    }
}
