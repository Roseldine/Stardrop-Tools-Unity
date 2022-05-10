
namespace StardropTools.Tween
{
    [UnityEngine.RequireComponent(typeof(TweenComponentFloat))]
    public class TweenComponentImagePixelPerUnitMultiplier : TweenComponentImage
    {
        [UnityEngine.Space]
        public TweenComponentFloat tween;

        public override void InitializeTween()
        {
            tween.OnTween.AddListener(UpdateValue);
            tween.InitializeTween();
        }

        public override void PauseTween() => tween.PauseTween();
        public override void StopTween() => tween.StopTween();

        public override void SetTweenID(int value)
        {
            base.SetTweenID(value);
            tween.tweenID = value;
            tween.tweenData = data;
        }

        void UpdateValue(float value) => target.pixelsPerUnitMultiplier = value;

        protected void OnValidate()
        {
            if (tween == null)
                tween = GetComponent<TweenComponentFloat>();

            tween.tweenData = data;
        }
    }
}