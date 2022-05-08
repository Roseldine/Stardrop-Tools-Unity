
namespace StardropTools.Tween
{
    public class TweenComponentPosition : TweenComponentTransformVector3
    {
        public enum PositionTweens
        {
            Position, LocalPosition,
            ShakePosition, ShakeLocalPosition,
        }

        [UnityEngine.Space]
        public PositionTweens tweenTarget;

        public override void InitializeTween()
        {
            base.InitializeTween();

            switch (tweenTarget)
            {
                case PositionTweens.Position:
                    if (HasStart)
                        tween = Tween.Position(target, startValue, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                        tween = Tween.Position(target, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                    break;

                case PositionTweens.LocalPosition:
                    if (HasStart)
                        tween = Tween.LocalPosition(target, startValue, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                        tween =Tween.LocalPosition(target, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                    break;

                case PositionTweens.ShakePosition:
                    tween = Tween.ShakePosition(target, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                    break;

                case PositionTweens.ShakeLocalPosition:
                    tween = Tween.ShakeLocalPosition(target, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
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
                case PositionTweens.Position:
                    startValue = target.position;
                    targetValue = target.position;
                    break;

                case PositionTweens.LocalPosition:
                    startValue = target.localPosition;
                    targetValue = target.localPosition;
                    break;

                case PositionTweens.ShakePosition:
                    targetValue = target.position;
                    break;

                case PositionTweens.ShakeLocalPosition:
                    targetValue = target.localPosition;
                    break;
            }

            copyStartValues = false;
        }
    }
}