public class Player : IObserver<GameEvent>
{
    public string Name { get; private set; }
    public List<Card> Hand { get; private set; } = new List<Card>();
    public List<Card> DiscardPile { get; private set; } = new List<Card>();
    public int Lives { get; set; }
    public bool HalveDamage { get; set; } = false;
    public bool SkipNextDrawingPhase { get; set; } = false;
    public Deck Deck { get; private set; } // Verplaats Deck naar de Player class
    private Game _game;

    public void OnNext(GameEvent value)
    {
        Console.WriteLine($"{Name} received update: {value.Message}");
    }

    public void OnError(Exception error)
    {
        
    }

    public void OnCompleted()
    {
    
    }

    // Verander de constructor om het Deck object correct te initialiseren
    public Player(string name, Game game)
    {
        Name = name;
        _game = game;
        Deck = new Deck(); // Initialiseer het Deck hier
        Lives = 10;
    }

    public void DrawCard()
    {
        if (SkipNextDrawingPhase)
        {
            Console.WriteLine($"{Name} skips the drawing phase.");
            SkipNextDrawingPhase = false;
            return;
        }

        var drawnCard = Deck.Draw(); // Gebruik het deck van de speler
        if (drawnCard != null)
        {
            Hand.Add(drawnCard);
            Console.WriteLine($"{Name} drew {drawnCard.Name}.");
            var gameEvent = new GameEvent($"{Name} drew a card.");
            _game.EventManager.NotifyObservers(gameEvent);
        }
        else
        {
            Console.WriteLine("No more cards to draw.");
        }
    }

    public void PlayCard(string cardName)
    {
        var cardToPlay = Hand.Find(card => card.Name == cardName);
        if (cardToPlay != null)
        {
            cardToPlay.Play();
            Hand.Remove(cardToPlay);
            Console.WriteLine($"{Name} played {cardName}.");
            var gameEvent = new GameEvent($"{Name} has played {cardName}.");
            _game.EventManager.NotifyObservers(gameEvent);

            if (cardToPlay is ArtifactCard artifact)
            {
                artifact.ActivateEffect(this, _game.GetOpponent(this), _game);
            }
        }
        else
        {
            Console.WriteLine($"{Name} does not have {cardName} in hand.");
        }
    }

    public void DiscardCard(Card card)
    {
        Hand.Remove(card);
        DiscardPile.Add(card);
        Console.WriteLine($"{Name} discarded {card.Name}.");
    }
}
