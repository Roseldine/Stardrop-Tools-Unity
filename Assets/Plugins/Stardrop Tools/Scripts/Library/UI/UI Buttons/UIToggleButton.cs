using System.Collections;
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleButton : UIButton
    {
        [SerializeField] protected bool value;
        [SerializeField] protected bool toggle;

        [Header("Toggle Components")]
        [SerializeField] RectTransform parentToggleComponents;
        [SerializeField] UIToggleComponent[] toggleComponents;
        [SerializeField] bool getComponents;

        public bool Value { get => value; }

        public readonly CoreEvent OnToggle = new CoreEvent();
        public readonly CoreEvent OnToggleTrue = new CoreEvent();
        public readonly CoreEvent OnToggleFalse = new CoreEvent();
        public readonly CoreEvent<bool> OnToggleBoolValue = new CoreEvent<bool>();

        public override void Initialize()
        {
            base.Initialize();
            Toggle(value);
        }

        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            OnClick.AddListener(Toggle);
            SubscribeToggleComponents();
        }

        public void Toggle()
        {
            value = !value;
            OnToggle?.Invoke();
            InvokeTrueOrFalse();
            OnToggleBoolValue?.Invoke(value);
        }

        public void Toggle(bool val)
        {
            value = val;
            OnToggle?.Invoke();
            InvokeTrueOrFalse();
            OnToggleBoolValue?.Invoke(value);
        }

        protected void SubscribeToggleComponents()
        {
            if (toggleComponents != null && toggleComponents.Length > 0)
                foreach (UIToggleComponent component in toggleComponents)
                    component.SubscribeToToggle(this);
        }

        protected void InvokeTrueOrFalse()
        {
            if (value)
                OnToggleTrue?.Invoke();
            else
                OnToggleFalse?.Invoke();
        }

        protected void GetToggleComponents()
        {
            if (parentToggleComponents == null)
                return;

            var comp = Utilities.GetItems<UIToggleComponent>(parentToggleComponents);
            toggleComponents = comp.ToArray();

            foreach (UIToggleComponent component in toggleComponents)
                    component.SetToggleTarget(this);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (getComponents)
            {
                GetToggleComponents();
                getComponents = false;
            }
        }
    }
}