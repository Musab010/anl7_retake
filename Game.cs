public class Game
{
    public EventManager EventManager { get; } = new EventManager();
    public List<Player> Players { get; } = new List<Player>();
    private CardFactory _cardFactory = new SimpleCardFactory();

    public Game()
    {
        // Geen directe verwijzingen naar GameStateManager hier, we gebruiken de singleton instance.
    }

    public void AddPlayer(string playerName)
    {
        var player = new Player(playerName, this);
        Players.Add(player);
        EventManager.Subscribe(player);
    }

    public void StartGame()
    {
        Console.WriteLine("Game has started.");

        foreach (var player in Players)
        {
            player.Deck.Shuffle(); // Schud het deck van elke speler
            Console.WriteLine($"{player.Name}'s deck size after shuffling: {player.Deck.Count()}");

            player.Lives = 10;
            for (int i = 0; i < 7; i++)
            {
                player.DrawCard();
            }

            // Controleer en toon de startcondities
            Console.WriteLine($"{player.Name} starts with {player.Hand.Count} cards in hand and {player.Lives} lives.");
            Console.WriteLine($"{player.Name}'s discard pile has {player.DiscardPile.Count} cards.");
            Console.WriteLine($"{player.Name}'s deck has {player.Deck.Count()} cards remaining.");
        }

        EventManager.NotifyObservers(new GameEvent("The game has officially begun!"));
        foreach (var player in Players)
        {
            SimulateTurn(player);
        }
    }


    public Player GetOpponent(Player currentPlayer)
    {
        var opponent = Players.Find(player => player != currentPlayer);
        if (opponent == null)
        {
            throw new InvalidOperationException("No opponent found. Ensure there are at least two players in the game.");
        }
        return opponent;
    }

    private void SimulateTurn(Player player)
    {
        Console.WriteLine($"{player.Name}'s turn starts.");
        player.DrawCard();

        // Speel een ArtifactCard als deze in de hand is
        var artifactCard = player.Hand.FirstOrDefault(card => card is ArtifactCard);
        if (artifactCard != null)
        {
            player.PlayCard(artifactCard.Name);
        }
        else if (player.Hand.Count > 0)
        {
            var cardToPlay = player.Hand[0];
            player.PlayCard(cardToPlay.Name);
            EventManager.NotifyObservers(new GameEvent($"{player.Name} played {cardToPlay.Name}."));
        }

        Console.WriteLine($"{player.Name}'s turn ends.");
    }
}
