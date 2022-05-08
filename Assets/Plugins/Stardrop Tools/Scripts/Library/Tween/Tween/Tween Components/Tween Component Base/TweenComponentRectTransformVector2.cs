
namespace StardropTools.Tween
{
    public abstract class TweenComponentRectTransformVector2 : TweenComponentVector2
    {
        public UnityEngine.RectTransform target;

        public override void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);
        }
    }
}