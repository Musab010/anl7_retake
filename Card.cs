// Musab Sivrikaya (0988932)
// Ozeir Moradi (0954800)
// Abstract base class for all types of cards
public abstract class Card
{
    public string Name { get; set; }
    public ICardState State { get; set; }

    protected Card(string name)
    {
        Name = name;
        State = new InHandState(); // Default state when the card is created
    }

    public void SetState(ICardState newState)
    {
        State = newState;
    }

    public virtual void Play()
    {
        SetState(new OnBattlefieldState());
        State.Handle(this);
    }

    public void Discard()
    {
        SetState(new DiscardedState());
        State.Handle(this);
    }
}

// Subclass for creature cards
public class CreatureCard : Card
{
    public int Attack { get; }
    public int Defense { get; }

    public CreatureCard(string name, int attack, int defense) : base(name)
    {
        Attack = attack;
        Defense = defense;
    }
}

// Subclass for land cards
public class LandCard : Card
{
    public LandCard(string name) : base(name) { }
}

// Subclass for spell cards
public class SpellCard : Card
{
    public string Effect { get; }

    public SpellCard(string name, string effect) : base(name)
    {
        Effect = effect;
    }
}

// Subclass for artifact cards
public class ArtifactCard : Card
{
    public string Effect { get; }

    public ArtifactCard(string name, string effect) : base(name)
    {
        Effect = effect;
    }

    public void ActivateEffect(Player owner, Player opponent, Game game)
    {
        Console.WriteLine($"{Name} artifact effect activated: {Effect}");

        if (Effect.Contains("all creatures can't defend"))
        {
            // Effect: all creatures cannot defend
            game.EventManager.NotifyObservers(new GameEvent("All creatures can't defend."));
            GameStateManager.Instance.PreventAllDefenses = true; // Use singleton instance to update game state
            Console.WriteLine("Effect applied: No creatures can defend.");
        }
        else if (Effect.Contains("half damage"))
        {
            // Effect: opponent's creatures deal half damage
            game.EventManager.NotifyObservers(new GameEvent("Opponent's creatures deal half damage."));
            opponent.HalveDamage = true;
            Console.WriteLine("Effect applied: Opponent's creatures deal half damage.");
        }
        else if (Effect.Contains("skip drawing phase"))
        {
            // Effect: opponent skips their next drawing phase
            game.EventManager.NotifyObservers(new GameEvent("Opponent will skip their drawing phase."));
            opponent.SkipNextDrawingPhase = true;
            GameStateManager.Instance.ScheduleArtifactDestruction(this); // Use singleton instance for scheduling artifact destruction
            Console.WriteLine("Effect applied: Opponent will skip their next drawing phase.");
        }
    }

    public override void Play()
    {
        Console.WriteLine($"{Name} is being played as an Artifact.");
        base.Play(); // Change state to OnBattlefield
        Console.WriteLine($"{Name} is now on the battlefield.");
    }
}
