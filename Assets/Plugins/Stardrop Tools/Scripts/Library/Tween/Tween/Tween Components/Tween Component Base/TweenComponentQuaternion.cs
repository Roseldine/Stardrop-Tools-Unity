
namespace StardropTools.Tween
{
    [System.Serializable]
    public abstract class TweenComponentQuaternion : TweenComponentBase
    {
        [UnityEngine.Space]
        public UnityEngine.Quaternion startValue;
        public UnityEngine.Quaternion targetValue;

        public static CoreEvent<UnityEngine.Quaternion> OnTween = new CoreEvent<UnityEngine.Quaternion>();

        public override void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);
            tween = Tween.Quaternion(startValue, targetValue, Duration, Delay, IgnoreTimeScale, curve, Loop, tweenID, OnTween);
        }
    }
}