
namespace StardropTools.Tween
{
    public abstract class TweenComponentRectTransformVector3 : TweenComponentVector3
    {
        public UnityEngine.RectTransform target;

        public override void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);
        }
    }
}