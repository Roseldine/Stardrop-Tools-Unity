﻿
namespace StardropTools
{
    [System.Serializable]
    public class CoreEventTriggerBase
    {
        [UnityEngine.SerializeField] protected string eventName;
        [UnityEngine.SerializeField] protected int eventID;
        [UnityEngine.SerializeField] protected bool triggered;

        public string EventName { get => eventName; set => eventName = value; }
        public int EventID { get => eventID; set => eventID = value; }
        public bool Triggered { get => triggered; }


        public static readonly BaseEvent<string> OnEventName = new BaseEvent<string>();
        public static readonly BaseEvent<int> OnEventID = new BaseEvent<int>();


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
}

