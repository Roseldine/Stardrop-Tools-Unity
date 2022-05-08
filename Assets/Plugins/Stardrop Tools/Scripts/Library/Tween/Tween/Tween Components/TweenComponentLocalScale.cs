
namespace StardropTools.Tween
{
    public class TweenComponentLocalScale : TweenComponentTransformVector3
    {
        public enum LocalScaleTweens
        {
            LocalScale,
            ShakeLocalScale,
        }

        [UnityEngine.Space]
        public LocalScaleTweens tweenTarget;

        public override void InitializeTween()
        {
            base.InitializeTween();

            switch (tweenTarget)
            {
                case LocalScaleTweens.LocalScale:
                    if (HasStart)
                        tween = Tween.LocalScale(target, startValue, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                        tween = Tween.LocalScale(target, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                    break;

                case LocalScaleTweens.ShakeLocalScale:
                        tween = Tween.ShakeLocalScale(target, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
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

            startValue = target.localScale;
            targetValue = target.localScale;

            copyStartValues = false;
        }
    }
}