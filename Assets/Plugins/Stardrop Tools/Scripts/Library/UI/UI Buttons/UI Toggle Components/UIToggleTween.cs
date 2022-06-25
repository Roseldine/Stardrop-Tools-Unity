
using UnityEngine;
using StardropTools.Tween;

namespace StardropTools.UI
{
    public class UIToggleTween : UIToggleComponent
    {
        [Tooltip("0-false, 1-true")]
        [SerializeField] protected TweenCluster[] sequences;
        [SerializeField] bool getSequences;

        public override void SubscribeToToggle(UIToggleButton target)
        {
            base.SubscribeToToggle(target);
            target.OnToggleBoolValue.AddListener(ToggleTweens);
        }

        public void ToggleTweens(bool val)
        {
            if (val == false)
                sequences[0].StartTweens();
            else
                sequences[1].StartTweens();
        }

        private void OnValidate()
        {
            if (getSequences)
            {
                sequences = Utilities.GetItems<TweenCluster>(transform).ToArray();
                getSequences = false;
            }
        }
    }
}


