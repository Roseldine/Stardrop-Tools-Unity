using System.Collections;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenComponentRecTransform : TweenComponentVector3
    {
        public RectTransform target;

        protected override void OnValidate()
        {
            base.OnValidate();

            if (copyStartValues == false)
                return;

            if (tweenType == Tween.TweenType.SizeDelta)
            {
                startValue = target.sizeDelta;
                targetValue = target.sizeDelta;
            }

            else if (tweenType == Tween.TweenType.AnchoredPosition)
            {
                startValue = target.anchoredPosition;
                targetValue = target.anchoredPosition;
            }

            else if (tweenType == Tween.TweenType.ShakeAnchoredPosition)
            {
                startValue = target.eulerAngles;
                targetValue = target.eulerAngles;
            }

            copyStartValues = false;
        }
    }
}