// State pattern
public interface ICardState
{
    void Handle(Card card);
}

public class InHandState : ICardState
{
    public void Handle(Card card)
    {
        Console.WriteLine($"{card.Name} is in hand.");
    }

    public void Play(Card card)
    {
        card.State = new OnBattlefieldState();
        card.State.Handle(card);
    }
}

public class OnBattlefieldState : ICardState
{
    public void Handle(Card card)
    {
        Console.WriteLine($"{card.Name} is on the battlefield.");
    }

    public void Attack(Card card)
    {
        card.State = new AttackingState();
        card.State.Handle(card);
    }
}

internal class AttackingState : ICardState
{
    public void Handle(Card card)
    {
        Console.WriteLine($"{card.Name} is attacking.");
    }

    public void Attack(Card card)
    {
        card.State = new AttackingState();
        card.State.Handle(card);
    }

}

public class DiscardedState : ICardState
{
    public void Handle(Card card)
    {
        Console.WriteLine($"{card.Name} has been discarded.");
    }

    public void Discard(Card card)
    {
        card.State = new DiscardedState();
        card.State.Handle(card);
    }
}
