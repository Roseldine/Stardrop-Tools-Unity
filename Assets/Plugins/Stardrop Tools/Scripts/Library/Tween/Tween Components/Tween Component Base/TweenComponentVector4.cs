
namespace StardropTools.Tween
{
    public class TweenComponentVector4 : TweenComponentValue
    {
        [UnityEngine.Space]
        public UnityEngine.Vector4 startValue;
        public UnityEngine.Vector4 targetValue;

        public static CoreEvent<UnityEngine.Vector4> OnTween = new CoreEvent<UnityEngine.Vector4>();

        public override void InitializeTween()
        {
            base.InitializeTween();
            tween = Tween.Vector4(startValue, targetValue, Duration, Delay, IgnoreTimeScale, curve, Loop, tweenID, OnTween);
        }
    }
}