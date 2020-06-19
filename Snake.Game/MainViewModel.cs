//using Snake.Contract;
using Snake.Game.Helpers;
using Snake.Game.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Snake.Game
{
    public class MainViewModel : ViewModelBase
    {
        private IKeyboardConfigurationProvider _keyboardConfigurationProvider;
        private IGameConfigurationProvider _gameConfigurationProvider;

        private ObservableCollection<SnakePart> _snake = new ObservableCollection<SnakePart>();

        public ObservableCollection<SnakePart> Snake
        {
            get
            {
                return _snake;
            }
            set => SetProperty(ref _snake, value);
        }

        private ObservableCollection<Food> _foodList = new ObservableCollection<Food>();

        public ObservableCollection<Food> FoodList
        {
            get
            {
                return _foodList;
            }
            set => SetProperty(ref _foodList, value);
        }

        private DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Render, Application.Current.Dispatcher);        
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


            //timer.Interval = TimeSpan.FromMilliseconds(1000 / gameConfigurationProvider.SnakeSpeed);
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += OnTimerTick;
            timer.Start();

            StartGame();
        }

        public ICommand OnKeyDownCommand { get; }

        public int GameAreaDimensionX { get; } = 400;

        public int GameAreaDimensionY { get; } = 400;

        //private SnakeDirection _snakeDirection = SnakeDirection.Up;

        private void StartGame()
        {
            _snake.Clear();
            var initialPositionX = GameAreaDimensionX / 2;
            var initialPositionY = GameAreaDimensionY / 2;
            //var headGeometry = new Rectangle(initialPositionX, initialPositionY, _gameConfigurationProvider.GameObjectSizeX, _gameConfigurationProvider.GameObjectSizeY);
            var head = new SnakePart(initialPositionX, initialPositionY, _gameConfigurationProvider.GameObjectSizeX, _gameConfigurationProvider.GameObjectSizeY, Brushes.LightGreen);
            _snake.Add(head);

            GenerateFood();
        }

        private void GenerateFood()
        {
            int maxPosX = GameAreaDimensionX / _gameConfigurationProvider.GameObjectSizeX;
            int maxPosY = GameAreaDimensionY / _gameConfigurationProvider.GameObjectSizeY;

            int foodPosX = _random.Next(0, maxPosX);
            int foodPosY = _random.Next(0, maxPosY);

            var food = new Food(foodPosX, foodPosY, _gameConfigurationProvider.GameObjectSizeX, _gameConfigurationProvider.GameObjectSizeY, Brushes.Red);
            FoodList.Add(food);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            MoveSnake();            
        }

        private void MoveSnake()
        {
            for (int i = Snake.Count - 1; i >= 0; --i)
            {
                //Move head
                if (i == 0)
                {
                    switch (GameState.SnakeDirection)
                    {
                        case SnakeDirection.Right:
                            Snake[i].X++;
                            break;
                        case SnakeDirection.Left:
                            Snake[i].X--;
                            break;
                        case SnakeDirection.Up:
                            Snake[i].Y--;
                            break;
                        case SnakeDirection.Down:
                            Snake[i].Y++;
                            break;
                    }

                    //Get maximum X and Y Pos
                    int maxXPos = GameAreaDimensionX / _gameConfigurationProvider.GameObjectSizeX;
                    int maxYPos = GameAreaDimensionY / _gameConfigurationProvider.GameObjectSizeY;

                    //Detect collission with game borders.
                    if (Snake[i].X < 0 || Snake[i].Y < 0
                        || Snake[i].X >= maxXPos || Snake[i].Y >= maxYPos)
                    {
                        //Die();
                    }


                    //Detect collission with body
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X &&
                           Snake[i].Y == Snake[j].Y)
                        {
                            //Die();
                        }
                    }

                    //Detect collision with food piece
                    if (Snake[0].X == FoodList[0].X && Snake[0].Y == FoodList[0].Y)
                    {
                        //Eat();
                    }

                }
                else
                {
                    //Move body
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        private void DisplayGraphics()
        {
            //Graphics canvas = 
        }

        //private SnakeDirection GetDirectionForPressedKey(Key key)
        //{
        //    if (_allowedMovementKeysMap.ContainsKey(key))
        //    {
        //        var newDirection = _allowedMovementKeysMap[key];
        //        GameState.SnakeDirection = newDirection;
        //        return newDirection;
        //    }

        //    return GameState.SnakeDirection;
        //}

        private void OnKeyDown(Key pressedKey)
        {
            //var direction = GetDirectionForPressedKey(pressedKey);
            if (_allowedMovementKeysMap.ContainsKey(pressedKey))
            {
                var newDirection = _allowedMovementKeysMap[pressedKey];
                GameState.SnakeDirection = newDirection;
            }

        }
    }
}
