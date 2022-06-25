
using UnityEngine;

namespace StardropTools.UI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class UIMenu : BaseUIObject
    {
        public int menuID;
        public bool IsOpen { get; protected set; }

        public override void Initialize()
        {
            base.Initialize();
            OnEnabled.AddListener(Open);
        }

        public virtual void Open()
        {
            SetActive(true);
            IsOpen = true;
        }

        public virtual void Close()
        {
            SetActive(false);
            IsOpen = false;
        }
    }
}