
namespace StardropTools.Tween
{
    public class TweenComponentVector3 : TweenComponentValue
    {
        [UnityEngine.Space]
        public UnityEngine.Vector3 startValue;
        public UnityEngine.Vector3 targetValue;

        public readonly CoreEvent<UnityEngine.Vector3> OnTween = new CoreEvent<UnityEngine.Vector3>();

        public override void InitializeTween()
        {
            base.InitializeTween();
            tween = Tween.Vector3(startValue, targetValue, Duration, Delay, IgnoreTimeScale, curve, Loop, tweenID, OnTween);
        }
    }
}