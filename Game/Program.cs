using Game;

var gameStartupOptions = GameStartupOptions.Create(
    title: "Green Alert"
);
using var game = new GameClass(gameStartupOptions);
game.Run();