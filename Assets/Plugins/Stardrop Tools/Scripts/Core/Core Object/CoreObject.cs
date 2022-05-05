
using UnityEngine;

namespace StardropTools
{
    public abstract class CoreObject : MonoBehaviour
    {
        [Header("Initialization")]
        [SerializeField] protected CoreObjectData coreData;

        public bool IsInitialized { get => coreData.IsInitialized; }
        public bool IsLateInitialized { get => coreData.IsLateInitialized; }
        public bool IsActive { get => coreData.IsActive; set => coreData.SetActive(value); }
        public bool CanUpdate { get => coreData.CanUpdate; set => coreData.SetUpdate(value); }
        public bool DrawGizmos { get => coreData.DrawGizmos; }

        public GameObject GameObject { get => coreData.GameObject; }
        public Transform Transform { get => coreData.Transform; }

        public Vector3 Position { get => Transform.position; set => Transform.position = value; }
        public Vector3 LocalPosition { get => Transform.localPosition; set => Transform.localPosition = value; }

        public Quaternion Rotation { get => Transform.rotation; set => Transform.rotation = value; }
        public Quaternion LocalRotation { get => Transform.localRotation; set => Transform.localRotation = value; }

        public Vector3 EulerRotation { get => Transform.eulerAngles; set => Transform.eulerAngles = value; }
        public Vector3 LocalEulerRotation { get => Transform.localEulerAngles; set => Transform.localEulerAngles = value; }

        public Vector3 Scale { get => Transform.localScale; set => Transform.localScale = value; }


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

        protected virtual void Awake()
        {
            DataCheck();

            if (coreData.Initialization == CoreObjectData.InitializationType.awake)
                Initialize();

            if (coreData.LateInitialization == CoreObjectData.InitializationType.awake)
                LateInitialize();
        }

        protected virtual void Start()
        {
            DataCheck();

            if (coreData.Initialization == CoreObjectData.InitializationType.start)
                Initialize();

            if (coreData.LateInitialization == CoreObjectData.InitializationType.start)
                LateInitialize();
        }

        public virtual void Initialize()
        {
            if (IsInitialized)
                return;

            DataCheck();
            coreData.Initialize();
        }


        public virtual void Initialize(bool canUpdate)
        {
            if (IsInitialized)
                return;

            SetCanUpdate(canUpdate);
            Initialize();
        }

        public virtual void LateInitialize()
        {
            if (IsLateInitialized)
                return;

            DataCheck();
            coreData.LateInitialize();
        }

        public virtual void SubscribeToEvents() { }

        public virtual void UnsubscribeFromEvents() { }

        public virtual void UnsubscribeFromEventsOnDisable() { }

        public virtual void UpdateResource()
        {
            if (IsInitialized == false || CanUpdate == false)
                return;
        }

        public virtual void LateUpdateResource()
        {
            if (IsInitialized == false || CanUpdate == false)
                return;
        }

        public virtual void FixedUpdateResource()
        {
            if (IsInitialized == false || CanUpdate == false)
                return;
        }

        public virtual void ResetResource()
        {
            if (IsInitialized == false)
                return;
        }

        public virtual void SetCanDrawGizmos(bool value) => coreData.DrawGizmos = value;

        protected virtual void OnEnable() { }
        protected virtual void OnDisable()
        {
            UnsubscribeFromEvents();
            UnsubscribeFromEventsOnDisable();
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
            else
                Print("Object unassigned");
        }

        public void SetCanUpdate(bool value) => coreData.SetUpdate(value);

        public void SetParent(Transform parent)
        {
            if (Transform.parent != parent)
                Transform.parent = parent;
            else
                Print(name + " is already child of " + parent);
        }

        public void SetChild(Transform child)
        {
            if (child.parent != Transform)
                child.parent = Transform;
            else
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


