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

    // Methode om een header te printen
    private void PrintHeader(string title)
    {
        Console.WriteLine("\n" + new string('=', 40));
        Console.WriteLine($"= {title}");
        Console.WriteLine(new string('=', 40) + "\n");
    }

    // Initialiseer de Huidige Spelstaat voor Turn 2
    public void InitializeStateForTurn2()
    {
        PrintHeader("Initializing game state for turn 2...");

        foreach (var player in Players)
        {
            player.Lives = 10;

            // Stel de handen van de spelers in met enkele kaarten
            player.Hand.Clear();
            player.Hand.Add(new CreatureCard("Griffin", 4, 4));
            player.Hand.Add(new SpellCard("Lightning Bolt", "Deal 4 damage"));
            player.Hand.Add(new ArtifactCard("Ancient Machine", "Opponent's creatures deal half damage"));
            player.Hand.Add(new LandCard("Forest"));
            player.Hand.Add(new CreatureCard("Griffin", 4, 4));
            player.Hand.Add(new ArtifactCard("Ancient Machine", "All creatures can't defend"));
            player.Hand.Add(new LandCard("Forest"));

            // Toon de startcondities
            Console.WriteLine($"{player.Name} has {player.Hand.Count} cards in hand and {player.Lives} lives.");
            Console.WriteLine($"{player.Name}'s discard pile has {player.DiscardPile.Count} cards.");
        }

        Console.WriteLine("\nSetting up initial cards on the battlefield...");

        EventManager.NotifyObservers(new GameEvent("State initialized for turn 2."));
    }

    // Simuleer Beurt 2
    public void SimulateTurn2()
    {
        PrintHeader("Simulating turn 2...");

        foreach (var player in Players)
        {
            Console.WriteLine($"\n{player.Name}'s turn starts.");
            player.DrawCard();

            // Speel de eerste kaart in de hand (voor demonstratie)
            if (player.Hand.Count > 0)
            {
                var cardToPlay = player.Hand[0];
                player.PlayCard(cardToPlay.Name);
            }

            Console.WriteLine($"{player.Name}'s turn ends.");
        }
    }

    public void StartGame()
    {
        PrintHeader("Game has started");

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
