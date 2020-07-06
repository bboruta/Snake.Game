## Snake in WPF

This is a very simple project of a Snake written in WPF in MVVM way with some attempt to use Hexagonal architecture.

It is a bit over engineered (for such a simple game) but still can be refactored a bit (MainViewModel for example).

The game is a very classic snake:
- snake eats food and grows
- snake dies upon hitting game boundaries
- snake dies upon hitting itself
- when snake grows, a random snake image is downloaded (random is picked from given links in config, image is downloaded every time in the background, no caching etc.)
- you can edit keymap in the configuration file (no checking if key exists in the system though)

Screenshot:
![alt text](https://github.com/bboruta/Snake.Game/blob/master/snake.PNG)

Feel free to share any thoughts :)