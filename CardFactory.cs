using System;
// Factory Method Pattern
public abstract class CardFactory
{
    public abstract Card CreateCard(string type);
}

public class SimpleCardFactory : CardFactory
{
    public override Card CreateCard(string type)
    {
        return type switch
        {
            "Creature" => new CreatureCard("Dragon", 5, 5), 
            "Land" => new LandCard("Mountain"), 
            "Spell" => new SpellCard("Fireball", "Deal 3 damage"), 
            "Artifact" => new ArtifactCard("Ancient Machine", "Effect Description Here"), // Voeg artifact creatie toe
            _ => throw new ArgumentException($"Invalid card type: {type}"),
        };
    }
}
