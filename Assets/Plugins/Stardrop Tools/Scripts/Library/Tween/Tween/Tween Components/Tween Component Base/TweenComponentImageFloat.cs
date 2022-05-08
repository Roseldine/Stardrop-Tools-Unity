
namespace StardropTools.Tween
{
    public abstract class TweenComponentImageFloat : TweenComponentFloat
    {
        public UnityEngine.UI.Image target;

        public override void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);
        }
    }
}