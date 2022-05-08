
namespace StardropTools.Tween
{
    [System.Serializable]
    public abstract class TweenComponentVector4 : TweenComponentBase
    {
        [UnityEngine.Space]
        public UnityEngine.Vector4 startValue;
        public UnityEngine.Vector4 targetValue;

        public static CoreEvent<UnityEngine.Vector4> OnTween = new CoreEvent<UnityEngine.Vector4>();

        public override void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);
            tween = Tween.Vector4(startValue, targetValue, Duration, Delay, IgnoreTimeScale, curve, Loop, tweenID, OnTween);
        }
    }
}