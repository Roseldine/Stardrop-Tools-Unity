
namespace StardropTools.Tween
{
    public class TweenComponentRotationEuler : TweenComponentTransformVector3
    {
        public enum RotationEulerTweens
        {
            RotationEuler, LocalRotationEuler,
            ShakeRotationEuler, ShakeLocalRotationEuler,

        }

        [UnityEngine.Space]
        public RotationEulerTweens tweenTarget;

        public readonly CoreEvent<UnityEngine.Quaternion> OnTweenQuaternion = new CoreEvent<UnityEngine.Quaternion>();

        public override void InitializeTween()
        {
            base.InitializeTween();

            switch (tweenTarget)
            {
                case RotationEulerTweens.RotationEuler:
                    if (HasStart)
                        tween = Tween.RotationEuler(target, startValue, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                        tween = Tween.RotationEuler(target, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                    break;

                case RotationEulerTweens.LocalRotationEuler:
                    if (HasStart)
                        tween = Tween.LocalRotationEuler(target, startValue, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                        tween = Tween.LocalRotationEuler(target, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                    break;

                case RotationEulerTweens.ShakeRotationEuler:
                    tween = Tween.ShakeRotation(target, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTweenQuaternion);
                    break;

                case RotationEulerTweens.ShakeLocalRotationEuler:
                    tween = Tween.ShakeLocalRotation(target, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTweenQuaternion);
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
                case RotationEulerTweens.RotationEuler:
                    startValue = target.eulerAngles;
                    targetValue = target.eulerAngles;
                    break;

                case RotationEulerTweens.LocalRotationEuler:
                    startValue = target.localEulerAngles;
                    targetValue = target.localEulerAngles;
                    break;

                case RotationEulerTweens.ShakeRotationEuler:
                    targetValue = target.eulerAngles;
                    break;

                case RotationEulerTweens.ShakeLocalRotationEuler:
                    targetValue = target.localEulerAngles;
                    break;
            }

            copyStartValues = false;
        }
    }
}