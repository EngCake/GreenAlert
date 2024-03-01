using Nez;

namespace Game;

internal struct GameStartupOptions {
    public string Title { get; private set; }

    public static GameStartupOptions Create(string title) { 
        return new GameStartupOptions {
            Title = title 
        }; 
    }
}

internal class GameClass : Core {
    public GameClass(GameStartupOptions gameStartupOptions): base(windowTitle: gameStartupOptions.Title) {
    }
}
