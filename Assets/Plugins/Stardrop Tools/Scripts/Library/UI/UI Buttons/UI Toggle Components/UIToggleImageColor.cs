
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleImageColor : UIToggleComponentWithAnimation
    {
        [Header("Color")]
        [SerializeField] UnityEngine.UI.Image[] images;
        [Tooltip("0-false, 1-true")]
        [SerializeField] Color[] colors = { Color.white, Color.white };
        

        public override void SubscribeToToggle(UIToggleButton target)
        {
            base.SubscribeToToggle(target);

            target.OnToggleBoolValue.AddListener(ToggleImageColor);
        }

        public void ToggleImageColor(bool val)
        {
            if (val == false)
                SetColors(colors[0]);
            else
                SetColors(colors[1]);
        }

        void SetColors(Color color)
        {
            if (images.Length == 0)
                return;

            if (animTime > 0)
            {
                StopAnimCR();
                animCR = StartCoroutine(AnimCR(color));
            }

            else
                for (int i = 0; i < images.Length; i++)
                    images[i].color = color;
        }

        System.Collections.IEnumerator AnimCR(Color color)
        {
            Color[] startColors = new Color[images.Length];
            for (int i = 0; i < startColors.Length; i++)
                startColors[i] = images[i].color;

            float t = 0;
            float percent = 0;

            while (t < animTime)
            {
                percent = t / animTime;

                for (int i = 0; i < images.Length; i++)
                    images[i].color = Color.LerpUnclamped(startColors[i], color, animCurve.Evaluate(percent));

                t += Time.deltaTime;
                yield return null;
            }

            for (int i = 0; i < images.Length; i++)
                images[i].color = color;
        }
    }
}


