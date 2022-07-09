
using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public class BaseObjectData
    {
        [SerializeField] protected GameObject selfObject;
        [SerializeField] protected Transform selfTransform;
        [Space]
        [SerializeField] protected bool isInitialized;
        [SerializeField] protected bool isLateInitialized;
        [SerializeField] protected bool isSubscribed;
        [Space]
        [SerializeField] protected bool isActive;
        [Space]
        public bool Debug;
        public bool DrawGizmos;


        public GameObject GameObject { get => selfObject; }
        public Transform Transform { get => selfTransform; }

        public bool IsInitialized { get => isInitialized; }
        public bool IsLateInitialized { get => isLateInitialized; }
        public bool IsSubscribed { get => isSubscribed; }

        public bool IsActive { get => isActive; }


        public BaseObjectData() { }
        public BaseObjectData(GameObject go, Transform trans)
        {
            selfObject = go;
            selfTransform = trans;
        }


        public virtual void Initialize()
        {
            if (isInitialized == false)
                isInitialized = true;
            else
                return;
        }

        public virtual void LateInitialize()
        {
            if (isLateInitialized == false)
                isLateInitialized = true;
            else
                return;
        }

        public virtual void SubscribeToEvents()
        {
            if (isInitialized == false)
                return;

            if (isSubscribed == false)
                isSubscribed = true;
            else
                return;
        }

        public virtual void UnsubscribeFromEvents()
        {
            if (isInitialized == false)
                return;

            if (isSubscribed == true)
                isSubscribed = false;
            else
                return;
        }

        public void SetActive(bool value) => isActive = value;

        public void SetGameObject(GameObject gameObject)
        {
            if (isInitialized)
                return;
            selfObject = gameObject;
        }

        public void SetTransform(Transform transform)
        {
            if (isInitialized)
                return;
            selfTransform = transform;
        }
    }
}