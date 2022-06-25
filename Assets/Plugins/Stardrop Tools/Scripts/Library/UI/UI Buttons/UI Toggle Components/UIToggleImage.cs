
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleImage : UIToggleComponent
    {
        [SerializeField] UnityEngine.UI.Image image;
        [Tooltip("0-false, 1-true")]
        [SerializeField] Sprite[] icons;

        public override void SubscribeToToggle(UIToggleButton target)
        {
            base.SubscribeToToggle(target);

            target.OnToggleBoolValue.AddListener(ToggleImage);
        }

        public void ToggleImage(bool val)
        {
            if (val == false)
                image.sprite = icons[0];
            else
                image.sprite = icons[1];
        }
    }
}


