using System;

// Musab Sivrikaya (0988932)
// Ozeir Moradi (0954800)
class Program
{
    static void Main(string[] args)
    {
        // Maak een nieuw spel aan
        Game game = new();

        // Voeg spelers toe aan het spel
        game.AddPlayer("Musab");
        game.AddPlayer("Ozeir");

        // Start het spel en informeer de spelers
        var gameStartEvent = new GameEvent("The game has started!");
        game.EventManager.NotifyObservers(gameStartEvent);

        // Initialiseer de spelstaat voor beurt 2
        game.InitializeStateForTurn2();

        // Simuleer beurt 2
        game.SimulateTurn2();
    }
}
