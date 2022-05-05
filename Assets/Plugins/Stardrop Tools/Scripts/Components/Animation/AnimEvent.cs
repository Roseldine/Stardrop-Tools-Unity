[System.Serializable]
public class AnimEvent
{
    [UnityEngine.SerializeField] CoreEventTrigger<int> eventTrigger;
    [UnityEngine.Range(0, 1)][UnityEngine.SerializeField] float time;

    public int ID { get => eventTrigger.EventID; set => eventTrigger.EventID = value; }
    public float Time { get => time; }
    public bool Triggered { get => eventTrigger.Triggered; }


    public void ResetInvoke() => eventTrigger.Reset();

    public void EventCheck(float timeDelta)
    {
        if (eventTrigger.Triggered == false && UnityEngine.Mathf.Approximately(timeDelta, time))
            InvokeEvent();
    }

    void InvokeEvent()
    {
        if (Triggered == false)
        {
            eventTrigger.Invoke(ID);
            UnityEngine.Debug.LogFormat("Event: {0} triggered!", ID);
        }
    }
}