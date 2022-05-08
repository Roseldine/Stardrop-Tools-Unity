
namespace StardropTools.Tween
{
    public abstract class TweenComponentTransformQuaternion : TweenComponentQuaternion
    {
        public UnityEngine.Transform target;

        public override void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);
        }
    }
}