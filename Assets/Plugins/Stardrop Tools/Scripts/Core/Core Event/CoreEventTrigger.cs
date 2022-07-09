
namespace StardropTools
{
    [System.Serializable]
    public class CoreEventTrigger : CoreEventTriggerBase
    {

        public static readonly BaseEvent OnEvent = new BaseEvent();


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

        public static readonly BaseEvent<T> OnEvent = new BaseEvent<T>();


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
}

