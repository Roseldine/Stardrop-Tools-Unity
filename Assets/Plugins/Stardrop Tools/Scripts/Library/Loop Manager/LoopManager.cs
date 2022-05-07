
using UnityEngine;
using StardropTools;

public class LoopManager : StardropTools.Singletons.SingletonCoreManager<LoopManager>
{
    [Header("Managers")]
    [SerializeField] Transform parentManagers;
    [SerializeField] CoreManager[] managers;
    [SerializeField] bool getManagers;

    public static readonly CoreEvent OnUpdate = new CoreEvent();
    public static readonly CoreEvent OnLateUpdate = new CoreEvent();
    public static readonly CoreEvent OnFixedUpdate = new CoreEvent();

    protected override void Awake()
    {
        Initialize();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public override void Initialize()
    {
        base.Initialize();
        SetCanUpdate(true);

        GetManagers();
        InitializeManagers();
        LateInitializeManagers();

        OnInitialize?.Invoke();
    }

    public override void LateInitialize()
    {
        base.LateInitialize();

        OnLateUpdate?.Invoke();
    }

    private void Update() => UpdateObject();

    private void FixedUpdate() => FixedUpdateObject();

    public override void UpdateObject()
    {
        UpdateManagers();
        OnUpdate?.Invoke();
    }

    public override void FixedUpdateObject()
    {
        FixedUpdateManagers();
        OnFixedUpdate?.Invoke();
    }


    #region Managers
    void GetManagers()
    {
        managers = GetItems<CoreManager>(parentManagers);
    }

    void InitializeManagers()
    {
        for (int i = 0; i < managers.Length; i++)
            managers[i].Initialize();
    }

    void LateInitializeManagers()
    {
        for (int i = 0; i < managers.Length; i++)
            if (managers[i].IsInitialized)
                managers[i].LateInitialize();
    }

    void UpdateManagers()
    {
        for (int i = 0; i < managers.Length; i++)
            if (managers[i].CanUpdate)
                managers[i].UpdateObject();
    }

    void FixedUpdateManagers()
    {
        for (int i = 0; i < managers.Length; i++)
            if (managers[i].CanUpdate)
                managers[i].FixedUpdateObject();
    }
    #endregion // Managers

    protected override void OnValidate()
    {
        base.OnValidate();

        if (getManagers)
        {
            GetManagers();
            getManagers = false;
        }
    }
}
