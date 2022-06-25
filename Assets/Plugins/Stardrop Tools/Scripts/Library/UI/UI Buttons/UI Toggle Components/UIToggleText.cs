
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleText : UIToggleComponent
    {
        [SerializeField] TMPro.TextMeshProUGUI text;
        [Tooltip("0-false, 1-true")]
        [SerializeField] string[] values;

        public override void SubscribeToToggle(UIToggleButton target)
        {
            base.SubscribeToToggle(target);

            target.OnToggleBoolValue.AddListener(ToggleText);
        }

        public void ToggleText(bool val)
        {
            if (val == false)
                text.text = values[0];
            else
                text.text = values[1];
        }
    }
}


