
namespace StardropTools.Tween
{
    public abstract class TweenComponentTransformVector4 : TweenComponentVector4
    {
        public UnityEngine.Transform target;

        public override void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);
        }
    }
}