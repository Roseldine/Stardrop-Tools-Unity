using System.Collections;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenComponentTransform : TweenComponentVector3
    {
        public Transform target;

        protected override void OnValidate()
        {
            base.OnValidate();

            if (copyStartValues == false)
                return;

            if (tweenType == Tween.TweenType.Position || tweenType == Tween.TweenType.ShakePosition)
            {
                startValue = target.position;
                targetValue = target.position;
            }

            else if (tweenType == Tween.TweenType.LocalPosition || tweenType == Tween.TweenType.ShakeLocalPosition)
            {
                startValue = target.localPosition;
                targetValue = target.localPosition;
            }

            else if (tweenType == Tween.TweenType.RotationEuler || tweenType == Tween.TweenType.ShakeRotationEuler)
            {
                startValue = target.eulerAngles;
                targetValue = target.eulerAngles;
            }

            else if (tweenType == Tween.TweenType.LocalRotationEuler || tweenType == Tween.TweenType.ShakeLocalRotationEuler)
            {
                startValue = target.localEulerAngles;
                targetValue = target.localEulerAngles;
            }

            else if (tweenType == Tween.TweenType.LocalScale || tweenType == Tween.TweenType.ShakeLocalScale)
            {
                startValue = target.localScale;
                targetValue = target.localScale;
            }

            copyStartValues = false;
        }
    }
}