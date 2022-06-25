
using UnityEngine;

namespace StardropTools.UI
{
    public abstract class UIToggleComponent : MonoBehaviour
    {
        [SerializeField] protected UIToggleButton toggle;

        public void SetToggleTarget(UIToggleButton target) => toggle = target;

        public virtual void SubscribeToToggle(UIToggleButton target)
        {
            if (toggle != target)
                toggle = target;
        }
    }
}


