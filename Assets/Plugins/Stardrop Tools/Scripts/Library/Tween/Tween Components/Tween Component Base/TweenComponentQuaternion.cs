
namespace StardropTools.Tween
{
    public class TweenComponentQuaternion : TweenComponentValue
    {
        [UnityEngine.Space]
        public UnityEngine.Quaternion startValue;
        public UnityEngine.Quaternion targetValue;

        public readonly CoreEvent<UnityEngine.Quaternion> OnTween = new CoreEvent<UnityEngine.Quaternion>();

        public override void InitializeTween()
        {
            base.InitializeTween();
            tween = Tween.Quaternion(startValue, targetValue, Duration, Delay, IgnoreTimeScale, curve, Loop, tweenID, OnTween);
        }
    }
}