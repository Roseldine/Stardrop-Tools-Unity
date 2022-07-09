
namespace StardropTools
{
    public class BaseComponent : UnityEngine.MonoBehaviour, IInitialize
    {
        public enum InitializationType { none, awake, start }

        [UnityEngine.Header("Initialization")]
        [UnityEngine.SerializeField] protected InitializationType initializationAt;
        [UnityEngine.SerializeField] protected InitializationType lateInitializationAt;

        public bool IsInitialized { get; protected set; }
        public bool IsLateInitialized { get; protected set; }

        public bool IsUpdating { get; protected set; }
        public bool IsFixedUpdating { get; protected set; }
        public bool IsLateUpdating { get; protected set; }


        #region Events

        public readonly BaseEvent OnInitialize = new BaseEvent();
        public readonly BaseEvent OnLateInitialize = new BaseEvent();

        public readonly BaseEvent OnUpdate = new BaseEvent();
        public readonly BaseEvent OnFixedUpdate = new BaseEvent();
        public readonly BaseEvent OnLateUpdate = new BaseEvent();

        public readonly BaseEvent OnAwake = new BaseEvent();
        public readonly BaseEvent OnStart = new BaseEvent();

        public readonly BaseEvent OnEnabled = new BaseEvent();
        public readonly BaseEvent OnDisabled = new BaseEvent();

        public readonly BaseEvent OnReset = new BaseEvent();

        #endregion // events

        #region Print & Debug.log
        /// <summary>
        /// substitute to Debug.Log();
        /// </summary>
        public static void Print(object message) => UnityEngine.Debug.Log(message);

        /// <summary>
        /// substitute to Debug.LogWarning();
        /// </summary>
        public static void PrintWarning(object message) => UnityEngine.Debug.LogWarning(message);
        #endregion // print


        public virtual void Initialize()
        {
            if (IsInitialized)
                return;

            IsInitialized = true;
            OnInitialize?.Invoke();
        }

        public virtual void LateInitialize()
        {
            if (IsLateInitialized)
                return;

            IsLateInitialized = true;
            OnLateInitialize?.Invoke();
        }


        // Start Update
        public virtual void StartUpdate()
        {
            if (IsUpdating)
                return;

            LoopManager.OnUpdate.AddListener(UpdateLogic);
            IsUpdating = true;
        }

        public virtual void StartFixedUpdate()
        {
            if (IsFixedUpdating)
                return;

            LoopManager.OnUpdate.AddListener(FixedUpdateLogic);
            IsFixedUpdating = true;
        }

        public virtual void StartLateUpdate()
        {
            if (IsLateUpdating)
                return;

            LoopManager.OnUpdate.AddListener(LateUpdateLogic);
            IsLateUpdating = true;
        }


        // Update
        public virtual void UpdateLogic()
            => OnUpdate?.Invoke();

        public virtual void FixedUpdateLogic()
            => OnFixedUpdate?.Invoke();

        public virtual void LateUpdateLogic()
            => OnLateUpdate?.Invoke();


        // Stop Update
        public virtual void StopUpdate()
        {
            if (IsUpdating == false)
                return;

            LoopManager.OnUpdate.RemoveListener(UpdateLogic);
            IsUpdating = false;
        }

        public virtual void StopFixedUpdate()
        {
            if (IsFixedUpdating == false)
                return;

            LoopManager.OnUpdate.RemoveListener(FixedUpdateLogic);
            IsFixedUpdating = false;
        }

        public virtual void StopLateUpdate()
        {
            if (IsLateUpdating == false)
                return;

            LoopManager.OnUpdate.RemoveListener(LateUpdateLogic);
            IsLateUpdating = false;
        }


        protected virtual void Awake()
        {
            if (initializationAt == InitializationType.awake)
                Initialize();

            if (lateInitializationAt == InitializationType.awake)
                LateInitialize();

            OnAwake?.Invoke();
        }

        protected virtual void Start()
        {
            if (initializationAt == InitializationType.start)
                Initialize();

            if (lateInitializationAt == InitializationType.start)
                LateInitialize();

            OnStart?.Invoke();
        }

        protected virtual void OnEnable()
        {
            OnEnabled.Invoke();
        }

        protected virtual void OnDisable()
        {
            StopUpdate();
            StopFixedUpdate();
            StopLateUpdate();

            OnDisabled?.Invoke();
        }

        public virtual void Reset()
        {
            IsInitialized = false;
            OnReset?.Invoke();
        }
    }
}