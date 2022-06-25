
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleTextColor : UIToggleComponentWithAnimation
    {
        [SerializeField] TMPro.TextMeshProUGUI[] texts;
        [Tooltip("0-false, 1-true")]
        [SerializeField] Color[] colors;

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
            if (texts.Length == 0)
                return;

            if (animTime > 0)
            {
                StopAnimCR();
                animCR = StartCoroutine(AnimCR(color));
            }

            else
                for (int i = 0; i < texts.Length; i++)
                    texts[i].color = color;
        }

        System.Collections.IEnumerator AnimCR(Color color)
        {
            Color[] startColors = new Color[texts.Length];
            for (int i = 0; i < startColors.Length; i++)
                startColors[i] = texts[i].color;

            float t = 0;
            float percent = 0;

            while (t < animTime)
            {
                percent = t / animTime;

                for (int i = 0; i < texts.Length; i++)
                    texts[i].color = Color.LerpUnclamped(startColors[i], color, animCurve.Evaluate(percent));

                t += Time.deltaTime;
                yield return null;
            }

            for (int i = 0; i < texts.Length; i++)
                texts[i].color = color;
        }
    }
}


