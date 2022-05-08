
namespace StardropTools.Tween
{
    [System.Serializable]
    public abstract class TweenComponentColor : TweenComponentBase
    {
        [UnityEngine.Space]
        public UnityEngine.Color startValue = UnityEngine.Color.white;
        public UnityEngine.Color targetValue = UnityEngine.Color.white;

        public readonly CoreEvent<UnityEngine.Color> OnTween = new CoreEvent<UnityEngine.Color>();

        public override void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);
            tween = Tween.Color(startValue, targetValue, Duration, Delay, IgnoreTimeScale, curve, Loop, tweenID, OnTween);
        }
    }
}