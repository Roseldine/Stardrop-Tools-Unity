
namespace StardropTools.Tween
{
    [System.Serializable]
    public abstract class TweenComponentVector2 : TweenComponentBase
    {
        [UnityEngine.Space]
        public UnityEngine.Vector2 startValue;
        public UnityEngine.Vector2 targetValue;

        public readonly CoreEvent<UnityEngine.Vector2> OnTween = new CoreEvent<UnityEngine.Vector2>();

        public override void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);
            tween = Tween.Vector2(startValue, targetValue, Duration, Delay, IgnoreTimeScale, curve, Loop, tweenID, OnTween);
        }
    }
}