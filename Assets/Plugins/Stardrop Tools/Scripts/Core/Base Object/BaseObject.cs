
using UnityEngine;

namespace StardropTools
{
    public class BaseObject : BaseComponent
    {
        [SerializeField] protected BaseObjectData coreData;

        #region Properties
        public bool IsActive { get => coreData.IsActive; set => SetActive(value); }
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


        // Position
        public float PositionX { get => Position.x; set => SetPositionX(value); }
        public float PositionY { get => Position.y; set => SetPositionY(value); }
        public float PositionZ { get => Position.z; set => SetPositionZ(value); }

        public float LocalPositionX { get => LocalPosition.x; set => SetLocalPositionX(value); }
        public float LocalPositionY { get => LocalPosition.y; set => SetLocalPositionY(value); }
        public float LocalPositionZ { get => LocalPosition.z; set => SetLocalPositionZ(value); }

        public Vector2 PositionXY { get => Position.GetXY(); set => Position = new Vector3(value.x, value.y, 0); }
        public Vector2 PositionXZ { get => Position.GetXZ(); set => Position = new Vector3(value.x, 0, value.y); }
        public Vector2 PositionYZ { get => Position.GetYZ(); set => Position = new Vector3(0, value.x, value.y); }


        // Rotation
        public float RotX { get => Rotation.x; }
        public float RotY { get => Rotation.y; }
        public float RotZ { get => Rotation.z; }
        public float RotW { get => Rotation.w; }

        public float LocalRotX { get => LocalRotation.x; }
        public float LocalRotY { get => LocalRotation.y; }
        public float LocalRotZ { get => LocalRotation.z; }
        public float LocalRotW { get => LocalRotation.w; }

        public float EulerX { get => EulerRotation.x; set => SetEulerX(value); }
        public float EulerY { get => EulerRotation.y; set => SetEulerY(value); }
        public float EulerZ { get => EulerRotation.z; set => SetEulerZ(value); }

        public float LocalEulerX { get => LocalRotation.x; set => SetLocalEulerX(value); }
        public float LocalEulerY { get => LocalRotation.y; set => SetLocalEulerY(value); }
        public float LocalEulerZ { get => LocalRotation.z; set => SetLocalEulerZ(value); }


        // Scale
        public float ScaleX { get => Scale.x; set => SetScaleX(value); }
        public float ScaleY { get => Scale.y; set => SetScaleY(value); }
        public float ScaleZ { get => Scale.z; set => SetScaleZ(value); }
        #endregion // properties

        #region Events
        
        public readonly BaseEvent OnSetParent = new BaseEvent();
        public readonly BaseEvent OnAddChild = new BaseEvent();
        public readonly BaseEvent OnRemoveChild = new BaseEvent();

        public readonly BaseEvent OnDestroyObject = new BaseEvent();

        public readonly BaseEvent OnActivate = new BaseEvent();
        public readonly BaseEvent OnDeactivate = new BaseEvent();

        #endregion // events


        public override void Initialize()
        {
            if (IsInitialized)
            {
                if (CanDebug)
                    Print(name + "is already Initialized");

                return;
            }

            InitialPosition = Transform.position;

            DataCheck();
            coreData.Initialize();
            IsInitialized = true;

            OnInitialize?.Invoke();
        }

        public override void LateInitialize()
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
            if (IsInitialized == false)
            {
                if (CanDebug && IsInitialized == false)
                    Print(name + " isn't Initialized");

                return false;
            }

            else
                return true;
        }

        public virtual void ResetObject()
        {
            if (IsInitialized == false)
                return;
        }

        public virtual void SetCanDrawGizmos(bool value) => coreData.DrawGizmos = value;

        protected override void OnEnable()
        {
            DataCheck();
            EnabledPosition = Transform.position;

            base.OnEnable();
        }

        protected override void OnDisable()
        {
            DisabledPosition = Transform.position;

            UnsubscribeFromEvents();
            UnsubscribeFromEventsOnDisable();

            base.OnDisable();
        }

        protected virtual void OnDestroy()
        {
            OnDestroyObject?.Invoke();
        }


        protected virtual void OnValidate()
        {
            if (coreData == null)
                coreData = new BaseObjectData();

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
                coreData = new BaseObjectData(gameObject, transform);
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

            if (value)
                OnActivate?.Invoke();
            else
                OnDeactivate?.Invoke();
        }

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



        // position
        public void SetPosition(Vector3 position)
            => Position = position;

        public void SetLocalPosition(Vector3 localPosition)
            => LocalPosition = localPosition;

        public void SetPositionX(float value)
            => Position = new Vector3(value, PositionY, PositionZ);

        public void SetPositionY(float value)
            => Position = new Vector3(PositionX, value, PositionZ);

        public void SetPositionZ(float value)
            => Position = new Vector3(PositionX, PositionY, value);


        public void SetLocalPositionX(float value)
            => LocalPosition = new Vector3(value, LocalPosition.y, LocalPosition.z);

        public void SetLocalPositionY(float value)
            => LocalPosition = new Vector3(LocalPosition.x, value, LocalPosition.z);

        public void SetLocalPositionZ(float value)
            => LocalPosition = new Vector3(LocalPosition.x, LocalPosition.y, value);


        // Rotation
        public void SetRotation(Quaternion rotation)
            => Rotation = rotation;

        public void SetLocalRotation(Quaternion localRotation)
            => LocalRotation = localRotation;


        public void SetEulerRotation(Vector3 rotation)
            => EulerRotation = rotation;

        public void SetLocalEulerRotation(Vector3 rotation)
            => LocalEulerRotation = rotation;


        public void SetEulerX(float x)
            => SetEulerRotation(new Vector3(x, EulerY, EulerZ));

        public void SetEulerY(float y)
            => SetEulerRotation(new Vector3(EulerX, y, EulerZ));

        public void SetEulerZ(float z)
            => SetEulerRotation(new Vector3(EulerX, EulerY, z));


        public void SetLocalEulerX(float x)
            => SetLocalEulerRotation(new Vector3(x, EulerY, EulerZ));
        public void SetLocalEulerY(float y)
            => SetLocalEulerRotation(new Vector3(EulerX, y, EulerZ));
        public void SetLocalEulerZ(float z)
            => SetLocalEulerRotation(new Vector3(EulerX, EulerY, z));


        // scale
        public void SetScale(Vector3 scale)
            => Scale = scale;

        public void SetScaleX(float x)
            => SetScale(new Vector3(x, ScaleY, ScaleZ));
        public void SetScaleY(float y)
            => SetScale(new Vector3(ScaleX, y, ScaleZ));
        public void SetScaleZ(float z)
            => SetScale(new Vector3(ScaleX, ScaleY, z));


        public Vector3 DirectionTo(Vector3 target) => target - Position;
        public Vector3 DirectionTo(Transform target) => target.position - Position;

        public Vector3 DirectionFrom(Vector3 target) => Position - target;
        public Vector3 DirectionFrom(Transform target) => Position - target.position;


        public float DistanceFrom(Vector3 target) => DirectionTo(target).magnitude;
        public float DistanceFrom(Transform target) => DirectionTo(target).magnitude;


        public Quaternion LookAt(Vector3 direction, bool lockX = true, bool lockY = false, bool lockZ = true)
        {
            if (direction == Vector3.zero)
            {
                //Debug.Log("Look rotation is zero");
                return Quaternion.identity;
            }

            Quaternion lookRot = Quaternion.LookRotation(direction);

            if (lockX) lookRot.x = 0;
            if (lockY) lookRot.y = 0;
            if (lockZ) lookRot.z = 0;

            SetRotation(lookRot);

            return lookRot;
        }

        public Quaternion LookAt(Transform target, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            Vector3 lookDir = DirectionTo(target.position);
            Quaternion targetRot = LookAt(lookDir, lockX, lockY, lockZ);

            return targetRot;
        }


        public Quaternion SmoothLookAt(Vector3 direction, float lookSpeed, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            if (direction == Vector3.zero)
                return Quaternion.identity;

            Quaternion lookRot = Quaternion.LookRotation(direction);
            Quaternion targetRot = Quaternion.Slerp(Rotation, lookRot, Time.deltaTime * lookSpeed);

            if (lockX) lookRot.x = 0;
            if (lockY) lookRot.y = 0;
            if (lockZ) lookRot.z = 0;

            SetRotation(targetRot);

            return targetRot;
        }

        public Quaternion SmoothLookAt(Transform target, float lookSpeed, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            Vector3 lookDir = DirectionTo(target.position);
            Quaternion targetRot = SmoothLookAt(lookDir, lookSpeed, lockX, lockY, lockZ);

            return targetRot;
        }
    }
}


