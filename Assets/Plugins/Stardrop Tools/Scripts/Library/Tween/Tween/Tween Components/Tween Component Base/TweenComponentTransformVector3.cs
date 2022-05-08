
namespace StardropTools.Tween
{
    public abstract class TweenComponentTransformVector3 : TweenComponentVector3
    {
        public UnityEngine.Transform target;

        public override void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);
        }
    }
}