class Program
{
    static void Main(string[] args)
    {
        Game game = new();
        game.AddPlayer("Musab");
        game.AddPlayer("Ozeir");

        var gameStartEvent = new GameEvent("The game has started!");
        game.EventManager.NotifyObservers(gameStartEvent);

        game.StartGame(); 
    }
}
