
using UnityEngine;

namespace StardropTools
{
    public abstract class CoreObject : MonoBehaviour
    {
        [Header("Initialization")]
        [SerializeField] protected CoreObjectData coreData;


        public bool IsInitialized { get => coreData.IsInitialized; }
        public bool IsLateInitialized { get => coreData.IsLateInitialized; }
        public bool IsActive { get => coreData.IsActive; set => SetActive(value); }
        public bool CanUpdate { get => coreData.CanUpdate; set => coreData.SetUpdate(value); }
        public bool CanDebug { get => coreData.Debug; }
        public bool DrawGizmos { get => coreData.DrawGizmos; }

        public GameObject GameObject { get => coreData.GameObject; }
        public Transform Transform { get => coreData.Transform; }
        public Transform Parent { get => Transform.parent; set => SetParent(value); }

        public Vector3 Position { get => Transform.position; set => Transform.position = value; }
        public Vector3 LocalPosition { get => Transform.localPosition; set => Transform.localPosition = value; }

        public Quaternion Rotation { get => Transform.rotation; set => Transform.rotation = value; }
        public Quaternion LocalRotation { get => Transform.localRotation; set => Transform.localRotation = value; }

        public Vector3 EulerRotation { get => Transform.eulerAngles; set => Transform.eulerAngles = value; }
        public Vector3 LocalEulerRotation { get => Transform.localEulerAngles; set => Transform.localEulerAngles = value; }

        public Vector3 Scale { get => Transform.localScale; set => Transform.localScale = value; }

        public Vector3 InitialPosition { get; private set; }
        public Vector3 EnabledPosition { get; private set; }
        public Vector3 DisabledPosition { get; private set; }


        #region Print & Debug.log
        /// <summary>
        /// substitute to Debug.Log();
        /// </summary>
        public static void Print(object message) => Debug.Log(message);

        /// <summary>
        /// substitute to Debug.LogWarning();
        /// </summary>
        public static void PrintWarning(object message) => Debug.LogWarning(message);
        #endregion // print

        #region Events
        public static readonly CoreEvent OnEnableObject = new CoreEvent();
        public static readonly CoreEvent OnDisableObject = new CoreEvent();

        public static readonly CoreEvent OnAwakeObject = new CoreEvent();
        public static readonly CoreEvent OnStartObject = new CoreEvent();

        public static readonly CoreEvent OnDestroyObject = new CoreEvent();

        public static readonly CoreEvent OnInitialize = new CoreEvent();
        public static readonly CoreEvent OnLateInitialize = new CoreEvent();

        public static readonly CoreEvent OnSetParent = new CoreEvent();
        public static readonly CoreEvent OnAddChild = new CoreEvent();
        public static readonly CoreEvent OnRemoveChild = new CoreEvent();
        #endregion // events


        protected virtual void Awake()
        {
            DataCheck();

            if (coreData.Initialization == CoreObjectData.InitializationType.awake)
                Initialize();

            if (coreData.LateInitialization == CoreObjectData.InitializationType.awake)
                LateInitialize();

            OnAwakeObject?.Invoke();
        }

        protected virtual void Start()
        {
            DataCheck();

            if (coreData.Initialization == CoreObjectData.InitializationType.start)
                Initialize();

            if (coreData.LateInitialization == CoreObjectData.InitializationType.start)
                LateInitialize();

            OnStartObject?.Invoke();
        }

        public virtual void Initialize()
        {
            if (IsInitialized)
                return;

            InitialPosition = Transform.position;

            DataCheck();
            coreData.Initialize();

            OnInitialize?.Invoke();
        }


        public virtual void Initialize(bool canUpdate)
        {
            if (IsInitialized)
            {
                if (CanDebug)
                    Print(name + "is already Initialized");

                return;
            }

            SetCanUpdate(canUpdate);
            Initialize();
        }

        public virtual void LateInitialize()
        {
            if (IsLateInitialized)
            {
                if (CanDebug)
                    Print(name + "is already LateInitialized");

                return;
            }

            DataCheck();
            coreData.LateInitialize();

            OnLateInitialize?.Invoke();
        }

        public virtual void SubscribeToEvents() { }

        public virtual void UnsubscribeFromEvents() { }

        public virtual void UnsubscribeFromEventsOnDisable() { }

        protected bool UpdateCheck()
        {
            if (IsInitialized == false || CanUpdate == false)
            {
                if (CanDebug && IsInitialized == false)
                    Print(name + " isn't Initialized");

                if (CanDebug && CanUpdate == false)
                    Print(name + " can't Update");

                return false;
            }

            else
                return true;
        }

        public virtual void UpdateObject()
        {
            if (UpdateCheck() == false)
                return;
        }

        public virtual void LateUpdateObject()
        {
            if (UpdateCheck() == false)
                return;
        }

        public virtual void FixedUpdateObject()
        {
            if (UpdateCheck() == false)
                return;
        }

        public virtual void HandleInput()
        {
            if (UpdateCheck() == false)
                return;
        }

        public virtual void ResetObject()
        {
            if (IsInitialized == false)
                return;
        }

        public virtual void SetCanDrawGizmos(bool value) => coreData.DrawGizmos = value;

        protected virtual void OnEnable()
        {
            EnabledPosition = Transform.position;

            OnEnableObject?.Invoke();
        }

        protected virtual void OnDisable()
        {
            DisabledPosition = Transform.position;

            UnsubscribeFromEvents();
            UnsubscribeFromEventsOnDisable();

            OnDisableObject?.Invoke();
        }

        protected virtual void OnDestroy()
        {
            OnDestroyObject?.Invoke();
        }


        protected virtual void OnValidate()
        {
            if (coreData == null)
                coreData = new CoreObjectData();

            if (GameObject == null)
                coreData.SetGameObject(gameObject);
            if (Transform == null)
                coreData.SetTransform(transform);
        }

        protected virtual void OnDrawGizmos()
        {
            if (DrawGizmos == false)
                return;
        }


        protected virtual void DataCheck()
        {
            if (coreData == null)
                coreData = new CoreObjectData(gameObject, transform);
        }


        //
        // Setters ------------------------------------------------------------------------------
        //

        public void SetLayer(int layerIndex) => GameObject.layer = layerIndex;

        public void SetActive(bool value)
        {
            coreData.SetActive(value);

            if (GameObject != null)
                GameObject.SetActive(value);
            else if (CanDebug)
                Print("Object unassigned");
        }

        public void SetCanUpdate(bool value) => coreData.SetUpdate(value);

        public void SetParent(Transform parent)
        {
            if (Transform.parent != parent)
            {
                Transform.parent = parent;
                OnSetParent?.Invoke();
            }

            else if (CanDebug)
                Print(name + " is already child of " + parent);
        }

        public void SetChild(Transform child)
        {
            if (child.parent != Transform)
            {
                child.parent = Transform;
                OnAddChild?.Invoke();
            }

            else if (CanDebug)
                Print(name + "is already parent of " + child);
        }

        public void SetChildIndex(Transform child, int index)
        {
            SetChild(child);
            child.SetSiblingIndex(index);
        }

        public void SetChildIndex(int childIndex, int index)
        {
            Transform child = Transform.GetChild(childIndex);
            child.SetSiblingIndex(index);
        }
    }
}


