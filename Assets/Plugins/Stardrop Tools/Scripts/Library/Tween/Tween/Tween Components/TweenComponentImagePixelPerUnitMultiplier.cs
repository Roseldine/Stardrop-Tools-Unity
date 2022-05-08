
namespace StardropTools.Tween
{
    public class TweenComponentImagePixelPerUnitMultiplier : TweenComponentImageFloat
    {
        public enum ImagePixelPerUnitMultiplierTweens
        {
            PixelsPerUnitMultiplier,
        }

        [UnityEngine.Space]
        public ImagePixelPerUnitMultiplierTweens tweenTarget;

        public override void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);

            switch (tweenTarget)
            {
                case ImagePixelPerUnitMultiplierTweens.PixelsPerUnitMultiplier:
                    if (HasStart)
                        tween = Tween.ImagePixelsPerUnitMultiplier(target, startValue, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
                        tween = Tween.ImagePixelsPerUnitMultiplier(target, targetValue, Duration, Delay, true, curve, Loop, tweenID, OnTween);
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

            startValue = target.pixelsPerUnitMultiplier;
            targetValue = target.pixelsPerUnitMultiplier;

            copyStartValues = false;
        }
    }
}