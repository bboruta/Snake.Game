using Snake.Contract;
using Snake.Contract.Models;
using Snake.Game.ExtensionMethods;
using Snake.Game.Helpers;
using Snake.Game.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
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
        private ICollisionDetector _collisionDetector;

        private Mutex _mutex = new Mutex();

        private ObservableCollection<SnakePartShape> _snake = new ObservableCollection<SnakePartShape>();
        private IEnumerable<SnakePart> _snakeForDomain;

        public ObservableCollection<SnakePartShape> Snake
        {
            get
            {
                return _snake;
            }
            set => SetProperty(ref _snake, value);
        }

        private ObservableCollection<FoodShape> _foodList = new ObservableCollection<FoodShape>();

        public ObservableCollection<FoodShape> FoodList
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
        private Dictionary<SnakeDirection, SnakeDirection> _oppositeDirections;

        private int _objectSizeX;
        private int _objectSizeY;
        private int _maxXPos;
        private int _maxYPos;

        public MainViewModel(
            IKeyboardConfigurationProvider keyboardConfigurationProvider,
            IGameConfigurationProvider gameConfigurationProvider,
            ICollisionDetector collisionDetector
            )
        {
            _keyboardConfigurationProvider = keyboardConfigurationProvider;
            _gameConfigurationProvider = gameConfigurationProvider;
            _collisionDetector = collisionDetector;

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

            _oppositeDirections = new Dictionary<SnakeDirection, SnakeDirection>()
            {
                {SnakeDirection.Up, SnakeDirection.Down },
                {SnakeDirection.Down, SnakeDirection.Up },
                {SnakeDirection.Left, SnakeDirection.Right },
                {SnakeDirection.Right, SnakeDirection.Left }
            };

            OnKeyDownCommand = new RelayCommandGeneric<Key>(p => OnKeyDown(p), p => true);

            timer.Interval = TimeSpan.FromMilliseconds(1000 / gameConfigurationProvider.SnakeSpeed);
            timer.Tick += OnTimerTick;
            timer.Start();

            _objectSizeX = _gameConfigurationProvider.GameObjectSizeX;
            _objectSizeY = _gameConfigurationProvider.GameObjectSizeY;
            _maxXPos = GameAreaDimensionX / _objectSizeX;
            _maxYPos = GameAreaDimensionY / _objectSizeY;
            
            StartGame();
        }

        public ICommand OnKeyDownCommand { get; }
        public int GameAreaDimensionX { get; } = 400;
        public int GameAreaDimensionY { get; } = 400;

        private void StartGame()
        {
            _snake.Clear();
            var initialPositionX = (GameAreaDimensionX / 2) / _objectSizeX * _objectSizeX;
            var initialPositionY = (GameAreaDimensionY / 2) / _objectSizeY * _objectSizeY;
            var head = new SnakePartShape(initialPositionX, initialPositionY, _objectSizeX, _objectSizeY, Brushes.LightGreen);
            _snake.Add(head);

            SpawnFood();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            MoveSnake();            
        }

        private void MoveSnake()
        {
            var mappedFood = FoodList.First().MapToFood();

            for (int i = Snake.Count - 1; i >= 0; --i)
            {
                //Move head
                if (i == 0)
                {
                    switch (GameState.SnakeDirection)
                    {
                        case SnakeDirection.Right:
                            Snake[i].X += _objectSizeX;
                            break;
                        case SnakeDirection.Left:
                            Snake[i].X -= _objectSizeX;
                            break;
                        case SnakeDirection.Up:
                            Snake[i].Y -= _objectSizeY;
                            break;
                        case SnakeDirection.Down:
                            Snake[i].Y += _objectSizeY;
                            break;
                    }

                    _snakeForDomain = Snake.MapToSnakePartCollection();
                    if (_collisionDetector.SnakeBorderCollisionHappened(_snakeForDomain, GameAreaDimensionX, GameAreaDimensionY, _objectSizeX, _objectSizeY))
                    {
                        Die();
                    }

                    if (_collisionDetector.SnakeHitsHimselfCollisionHappened(_snakeForDomain))
                    {
                        Die();
                    }
              
                    if (_collisionDetector.SnakeFoodCollisionHappened(_snakeForDomain, mappedFood))
                    {
                        Eat();
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

        private void Eat()
        {
            var bodyPart = new SnakePartShape(Snake.Last().X, Snake.Last().Y, _objectSizeX, _objectSizeY, Brushes.DarkGreen);
            _snake.Add(bodyPart);
            SpawnFood();
            //download image and display it
            DownloadImage();
        }

        private void Die()
        {
            //pokaz jakis napis ze game over
            // pokaz wynik moze
            // jesli nacisnie dowolny klawisz to restart i jakies male odliczanie 3,2,1
            // ale to juz na sam koniec
            StartGame();
        }

        private void SpawnFood()
        {
            // there is always one piece of food
            FoodList.Clear();
            var foodPosXLogical = _random.Next(0, _maxXPos);
            var foodPosYLogical = _random.Next(0, _maxYPos);
            var foodPosXPhysical = foodPosXLogical * _objectSizeX;
            var foodPosYPhysical = foodPosYLogical * _objectSizeY;

            var food = new FoodShape(foodPosXPhysical, foodPosYPhysical, _gameConfigurationProvider.GameObjectSizeX, _gameConfigurationProvider.GameObjectSizeY, Brushes.Red);
            FoodList.Add(food);
        }

        private void OnKeyDown(Key pressedKey)
        {
            if (_allowedMovementKeysMap.ContainsKey(pressedKey))
            {
                var newDirection = _allowedMovementKeysMap[pressedKey];
                if (_oppositeDirections[GameState.SnakeDirection] != newDirection)
                {
                    GameState.SnakeDirection = newDirection;
                }                
            }
        }

        private byte[] _snakeImage;

        public byte[] SnakeImage
        {
            get => _snakeImage;
            set
            {
                _mutex.WaitOne();
                SetProperty(ref _snakeImage, value);
                _mutex.ReleaseMutex();
            }
        }

        private int _currentImageIndex = 0;

        private void DownloadImage()
        {
            Thread DownloadImageCaller = new Thread(new ThreadStart(
                () =>
                {
                    if (_currentImageIndex == _gameConfigurationProvider.ImagesLinks.Count() - 1)
                    {
                        _currentImageIndex = _random.Next(0, _currentImageIndex);
                    }
                    else
                    {
                        var potentialImageIndexA = _random.Next(0, _currentImageIndex);
                        var potentialImageIndexB = _random.Next(_currentImageIndex + 1, _gameConfigurationProvider.ImagesLinks.Count());
                        _currentImageIndex = _random.Next(2) == 1 ? potentialImageIndexA : potentialImageIndexB;
                    }                    

                    var imageLink = _gameConfigurationProvider.ImagesLinks[_currentImageIndex];
                    var httpClient = new HttpClient();
                    var url = imageLink;
                    var buffer = httpClient.GetByteArrayAsync(url).Result;

                    using (var stream = new MemoryStream(buffer))
                    {
                        buffer = stream.ToArray();
                    }

                    SnakeImage = buffer;
                }
                ));

            DownloadImageCaller.Start();
        }
    }
}
