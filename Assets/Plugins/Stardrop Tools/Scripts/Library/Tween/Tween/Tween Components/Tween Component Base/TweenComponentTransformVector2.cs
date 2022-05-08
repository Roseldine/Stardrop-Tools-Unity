
namespace StardropTools.Tween
{
    public abstract class TweenComponentTransformVector2 : TweenComponentVector2
    {
        public UnityEngine.Transform target;

        public override void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);
        }
    }
}