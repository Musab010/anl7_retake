public class Deck
{
    private List<Card> _cards = new List<Card>();
    private Random _random = new Random();

    public Deck()
    {
        // Initialiseer het deck met 30 kaarten
        InitializeDeck();
    }

    public void InitializeDeck()
    {
        var cardFactory = new SimpleCardFactory();

        for (int i = 0; i < 10; i++) // Voeg 10 creature kaarten toe
        {
            var creatureCard = cardFactory.CreateCard("Creature");
            AddCard(creatureCard);
        }

        for (int i = 0; i < 10; i++) // Voeg 10 land kaarten toe
        {
            var landCard = cardFactory.CreateCard("Land");
            AddCard(landCard);
        }

        for (int i = 0; i < 5; i++) // Voeg 5 spell kaarten toe
        {
            var spellCard = cardFactory.CreateCard("Spell");
            AddCard(spellCard);
        }

        // Voeg 5 artifact kaarten toe met verschillende effecten
        AddCard(new ArtifactCard("Ancient Machine", "All creatures can't defend"));
        AddCard(new ArtifactCard("Ancient Machine", "Opponent's creatures deal half damage"));
        AddCard(new ArtifactCard("Ancient Machine", "Opponent will skip their drawing phase"));
        AddCard(new ArtifactCard("Ancient Machine", "All creatures can't defend")); // Extra voor diversiteit
        AddCard(new ArtifactCard("Ancient Machine", "Opponent's creatures deal half damage")); // Extra voor diversiteit
    }


    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    public Card? Draw()
    {
        if (_cards.Count > 0)
        {
            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }

        return null;
    }

    public void Shuffle()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            int randomIndex = _random.Next(i, _cards.Count);
            var temp = _cards[i];
            _cards[i] = _cards[randomIndex];
            _cards[randomIndex] = temp;
        }
    }

    // Nieuwe methode om het aantal kaarten in het deck te retourneren
    public int Count()
    {
        return _cards.Count;
    }

    // Debug methode om het deck te printen
    public void PrintDeck()
    {
        foreach (var card in _cards)
        {
            Console.WriteLine($"Card: {card.Name}");
        }
    }
}