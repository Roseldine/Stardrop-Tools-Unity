

namespace StardropTools.Tween
{
    public class TweenComponentRotation : TweenComponentTransformQuaternion
    {
        public enum RotationTweens
        {
            Rotation,
            LocalRotation,
        }

        [UnityEngine.Space]
        public RotationTweens tweenTarget;

        public override void InitializeTween()
        {
            base.InitializeTween();

            switch (tweenTarget)
            {
                case RotationTweens.Rotation:
                    if (HasStart)
                        tween = Tween.Rotation(target, startValue, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                        tween = Tween.Rotation(target, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                    break;

                case RotationTweens.LocalRotation:
                    if (HasStart)
                        tween = Tween.LocalRotation(target, startValue, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                        tween = Tween.LocalRotation(target, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                    break;
            }
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (target == null)
                return;

            if (copyStartValues == false)
                return;

            switch (tweenTarget)
            {
                case RotationTweens.Rotation:
                    startValue = target.rotation;
                    targetValue = target.rotation;
                    break;

                case RotationTweens.LocalRotation:
                    startValue = target.localRotation;
                    targetValue = target.localRotation;
                    break;
            }

            copyStartValues = false;
        }
    }
}