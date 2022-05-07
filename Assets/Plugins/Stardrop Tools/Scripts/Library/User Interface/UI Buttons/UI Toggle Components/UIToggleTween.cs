
using UnityEngine;
using StardropTools.TweenSurge;

namespace StardropTools.UI
{
    public class UIToggleTween : UIToggleComponent
    {
        [Tooltip("0-false, 1-true")]
        [SerializeField] protected TweenSurgeCluster[] sequences;
        [SerializeField] bool getSequences;

        public override void SubscribeToToggle(UIToggle target)
        {
            base.SubscribeToToggle(target);
            target.OnToggleValue.AddListener(ToggleTweens);
        }

        public void ToggleTweens(bool val)
        {
            if (val == false)
                sequences[0].InitializeTweens();
            else
                sequences[1].InitializeTweens();
        }

        private void OnValidate()
        {
            if (getSequences)
            {
                sequences = Utilities.GetItems<TweenSurgeCluster>(transform);
                getSequences = false;
            }
        }
    }
}


