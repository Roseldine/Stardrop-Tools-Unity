

public class LoopManager : Singleton<LoopManager>
{
    public bool IsInitialized { get; private set; }

    public static readonly BaseEvent OnAwake = new BaseEvent();
    public static readonly BaseEvent OnStart = new BaseEvent();
    public static readonly BaseEvent OnUpdate = new BaseEvent();
    public static readonly BaseEvent OnLateUpdate = new BaseEvent();
    public static readonly BaseEvent OnFixedUpdate = new BaseEvent();

    public static readonly BaseEvent OnEnabled = new BaseEvent();
    public static readonly BaseEvent OnDisabled = new BaseEvent();
    

    public void Initialize()
    {
        if (IsInitialized)
            return;

        IsInitialized = true;
    }


    private void Start() => OnStart?.Invoke();

    private void Update() => OnUpdate?.Invoke();

    private void FixedUpdate() => OnFixedUpdate?.Invoke();

    private void OnEnable() => OnEnabled?.Invoke();

    private void OnDisable() => OnDisabled?.Invoke();
}
