
[System.Serializable]
public abstract class CoreEventTriggerBase
{
    [UnityEngine.SerializeField] protected string eventName;
    [UnityEngine.SerializeField] protected int eventID;
    [UnityEngine.SerializeField] protected bool triggered;

    public string EventName { get => eventName; set => eventName = value; }
    public int EventID { get => eventID; set => eventID = value; }
    public bool Triggered { get => triggered; }


    public static readonly CoreEvent<string> OnEventName = new CoreEvent<string>();
    public static readonly CoreEvent<int> OnEventID = new CoreEvent<int>();


    public CoreEventTriggerBase() { }
    public CoreEventTriggerBase(string name) => eventName = name;
    public CoreEventTriggerBase(int id) => eventID = id;

    public CoreEventTriggerBase(string name, int id)
    {
        eventName = name;
        eventID = id;
    }

    public virtual void Invoke()
    {
        if (triggered)
            return;

        OnEventName?.Invoke(eventName);
        OnEventID?.Invoke(eventID);

        triggered = true;
    }

    public void Reset() => triggered = false;
}

[System.Serializable]
public class CoreEventTrigger : CoreEventTriggerBase
{

    public static readonly CoreEvent OnEvent = new CoreEvent();


    public CoreEventTrigger() { }
    public CoreEventTrigger(string name) => eventName = name;
    public CoreEventTrigger(int id) => eventID = id;

    public CoreEventTrigger(string name, int id)
    {
        eventName = name;
        eventID = id;
    }

    public override void Invoke()
    {
        base.Invoke();
        OnEvent?.Invoke();
    }
}

[System.Serializable]
public class CoreEventTrigger<T> : CoreEventTriggerBase
{

    public static readonly CoreEvent<T> OnEvent = new CoreEvent<T>();


    public CoreEventTrigger() { }
    public CoreEventTrigger(string name) => eventName = name;
    public CoreEventTrigger(int id) => eventID = id;

    public CoreEventTrigger(string name, int id)
    {
        eventName = name;
        eventID = id;
    }

    public void Invoke(T type)
    {
        base.Invoke();
        OnEvent?.Invoke(type);
    }
}