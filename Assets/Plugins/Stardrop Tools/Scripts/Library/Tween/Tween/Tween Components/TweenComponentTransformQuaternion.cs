using System.Collections;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenComponentTransformQuaternion : TweenComponentQuaternion
    {
        public Transform target;

        protected override void OnValidate()
        {
            base.OnValidate();

            if (copyStartValues == false)
                return;

            if (tweenType == Tween.TweenType.Rotation || tweenType == Tween.TweenType.ShakeRotation)
            {
                startValue = target.rotation;
                targetValue = target.rotation;
            }

            else if (tweenType == Tween.TweenType.LocalRotation || tweenType == Tween.TweenType.ShakeLocalRotation)
            {
                startValue = target.localRotation;
                targetValue = target.localRotation;
            }

            copyStartValues = false;
        }
    }
}