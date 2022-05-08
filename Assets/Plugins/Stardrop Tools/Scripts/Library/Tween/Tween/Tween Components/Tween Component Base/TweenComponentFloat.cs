
namespace StardropTools.Tween
{
    public class TweenComponentFloat : TweenComponentValue
    {
        [UnityEngine.Space]
        public float startValue;
        public float targetValue;

        public readonly CoreEvent<float> OnTween = new CoreEvent<float>();

        public override void InitializeTween()
        {
            base.InitializeTween();
            tween = Tween.Float(startValue, targetValue, Duration, Delay, IgnoreTimeScale, curve, Loop, tweenID, OnTween);
        }
    }
}