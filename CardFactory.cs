// Musab Sivrikaya (0988932)
// Ozeir Moradi (0954800)

using System;

// Abstract factory class for creating different types of cards
public abstract class CardFactory
{
    public abstract Card CreateCard(string type);
}
public class SimpleCardFactory : CardFactory
{
    // Override the CreateCard method to produce specific card instances based on the type
    public override Card CreateCard(string type)
    {
        return type switch
        {
            "Creature" => new CreatureCard("Griffin", 4, 4),
            "Land" => new LandCard("Forest"),
            "Spell" => new SpellCard("Lightning Bolt", "Deal 4 damage"),
            "Artifact" => new ArtifactCard("Ancient Machine", "Skip drawing phase"),
            _ => throw new ArgumentException($"Invalid card type: {type}"),
        };
    }
}
