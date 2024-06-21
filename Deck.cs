public class Deck
{
    private List<Card> _cards = new List<Card>(); 
    private Random _random = new Random(); 

    public Deck()
    {
        InitializeDeck();
    }

    public void InitializeDeck()
    {
        var cardFactory = new SimpleCardFactory();
        AddCardsOfType(cardFactory, "Creature", 10);
        AddCardsOfType(cardFactory, "Land", 10);
        AddCardsOfType(cardFactory, "Spell", 5);

        AddCard(new ArtifactCard("Ancient Machine", "All creatures can't defend"));
        AddCard(new ArtifactCard("Ancient Device", "Opponent's creatures deal half damage"));
        AddCard(new ArtifactCard("Timeless Relic", "Opponent will skip their drawing phase"));
        AddCard(new ArtifactCard("Eternal Gear", "All creatures can't defend"));
        AddCard(new ArtifactCard("Mystic Engine", "Opponent's creatures deal half damage"));
    }

    private void AddCardsOfType(SimpleCardFactory factory, string type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var card = factory.CreateCard(type);
            AddCard(card);
        }
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
        Console.WriteLine("No more cards to draw.");
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
        Console.WriteLine("Deck shuffled.");
    }

    public int Count()
    {
        return _cards.Count;
    }

    public void PrintDeck()
    {
        if (_cards.Count == 0)
        {
            Console.WriteLine("Deck is empty.");
        }
        else
        {
            Console.WriteLine("Current deck contains:");
            foreach (var card in _cards)
            {
                Console.WriteLine($"- Card: {card.Name}");
            }
        }
    }
}