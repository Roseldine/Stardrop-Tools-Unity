
namespace StardropTools.Tween
{
    public class TweenComponentImageColor : TweenComponentColor
    {
        public UnityEngine.UI.Image target;

        public enum ImageColorTweens
        {
            ImageColor,
        }

        [UnityEngine.Space]
        public ImageColorTweens tweenTarget;

        public override void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);

            switch (tweenTarget)
            {
                case ImageColorTweens.ImageColor:
                    if (HasStart)
                        tween = Tween.ImageColor(target, startValue, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                        tween = Tween.ImageColor(target, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
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

            startValue = target.color;
            targetValue = target.color;

            copyStartValues = false;
        }
    }
}