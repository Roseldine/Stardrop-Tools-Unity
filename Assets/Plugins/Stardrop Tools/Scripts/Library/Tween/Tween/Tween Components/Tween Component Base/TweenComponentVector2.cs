
namespace StardropTools.Tween
{
    public class TweenComponentVector2 : TweenComponentValue
    {
        [UnityEngine.Space]
        public UnityEngine.Vector2 startValue;
        public UnityEngine.Vector2 targetValue;

        public readonly CoreEvent<UnityEngine.Vector2> OnTween = new CoreEvent<UnityEngine.Vector2>();

        public override void InitializeTween()
        {
            base.InitializeTween();
            tween = Tween.Vector2(startValue, targetValue, Duration, Delay, IgnoreTimeScale, curve, Loop, tweenID, OnTween);
        }
    }
}