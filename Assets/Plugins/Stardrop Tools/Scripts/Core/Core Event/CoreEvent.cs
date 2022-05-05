
using System;

public class CoreEvent
{
    private event Action action = delegate { };

    public void Invoke() => action?.Invoke();

    public void AddListener(Action listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action listener) => action -= listener;

    public void RemoveAllListeners(CoreEvent evt)
    {
        Delegate[] eventListeners = evt.action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            evt.action -= del as Action;
    }
}

public class CoreEvent<T>
{
    private event Action<T> action = delegate { };

    public void Invoke(T param) => action?.Invoke(param);

    public void AddListener(Action<T> listener)
    {
        action -= listener; // does nothing if listener is not subscribed => prevents duplicate subscriptions
        action += listener;
    }

    public void RemoveListener(Action<T> listener) => action -= listener;

    public void RemoveAllListeners(CoreEvent<T> evt)
    {
        Delegate[] eventListeners = evt.action.GetInvocationList();
        foreach (Delegate del in eventListeners)
            evt.action -= del as Action<T>;
    }
}
