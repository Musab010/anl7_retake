using System;
using System.Collections.Generic;

public class EventManager
{
    private readonly List<IObserver<GameEvent>> _observers = new List<IObserver<GameEvent>>();

    public IDisposable Subscribe(IObserver<GameEvent> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        return new Unsubscriber(_observers, observer);
    }

    private class Unsubscriber : IDisposable
    {
        private readonly List<IObserver<GameEvent>> _observers;
        private readonly IObserver<GameEvent> _observer;

        public Unsubscriber(List<IObserver<GameEvent>> observers, IObserver<GameEvent> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }

    public void NotifyObservers(GameEvent gameEvent)
    {
        foreach (var observer in _observers)
        {
            observer.OnNext(gameEvent);
        }
    }
}
