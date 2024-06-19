public abstract class Card
{
    public string Name { get; set; }
    public ICardState State { get; set; }

    protected Card(string name)
    {
        Name = name;
        State = new InHandState(); // Default state
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

public class CreatureCard : Card
{
    public int Attack { get; set; }
    public int Defense { get; set; }

    public CreatureCard(string name, int attack, int defense) : base(name)
    {
        Attack = attack;
        Defense = defense;
    }
}

public class LandCard : Card
{
    public LandCard(string name) : base(name) { }
}

public class SpellCard : Card
{
    public string Effect { get; set; }

    public SpellCard(string name, string effect) : base(name)
    {
        Effect = effect;
    }
}

// Nieuwe ArtifactCard class
public class ArtifactCard : Card
{
    public string Effect { get; private set; }

    public ArtifactCard(string name, string effect) : base(name)
    {
        Effect = effect;
    }

    public void ActivateEffect(Player owner, Player opponent, Game game)
    {
        Console.WriteLine($"{Name} artifact effect activated: {Effect}");

        if (Effect.Contains("all creatures can't defend"))
        {
            // Effect: alle creatures kunnen niet verdedigen
            game.EventManager.NotifyObservers(new GameEvent("All creatures can't defend."));
            GameStateManager.Instance.PreventAllDefenses = true; // Gebruik singleton instance
            Console.WriteLine("Effect applied: No creatures can defend.");
        }
        else if (Effect.Contains("half damage"))
        {
            // Effect: alle creatures van de tegenstander doen halve schade
            game.EventManager.NotifyObservers(new GameEvent("Opponent's creatures deal half damage."));
            opponent.HalveDamage = true;
            Console.WriteLine("Effect applied: Opponent's creatures deal half damage.");
        }
        else if (Effect.Contains("skip drawing phase"))
        {
            // Effect: de tegenstander slaat hun volgende tekenfase over
            game.EventManager.NotifyObservers(new GameEvent("Opponent will skip their drawing phase."));
            opponent.SkipNextDrawingPhase = true;
            GameStateManager.Instance.ScheduleArtifactDestruction(this); // Gebruik singleton instance
            Console.WriteLine("Effect applied: Opponent will skip their next drawing phase.");
        }
    }

    public override void Play()
    {
        Console.WriteLine($"{Name} is being played as an Artifact.");
        SetState(new OnBattlefieldState());
        State.Handle(this);
        Console.WriteLine($"{Name} is now on the battlefield.");
    }
}







