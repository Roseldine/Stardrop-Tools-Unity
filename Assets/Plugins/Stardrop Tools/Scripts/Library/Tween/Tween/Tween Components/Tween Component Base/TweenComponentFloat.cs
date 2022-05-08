
namespace StardropTools.Tween
{
    [System.Serializable]
    public abstract class TweenComponentFloat : TweenComponentBase
    {
        [UnityEngine.Space]
        public float startValue;
        public float targetValue;

        public readonly CoreEvent<float> OnTween = new CoreEvent<float>();

        public override void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);
            tween = Tween.Float(startValue, targetValue, Duration, Delay, IgnoreTimeScale, curve, Loop, tweenID, OnTween);
        }
    }
}