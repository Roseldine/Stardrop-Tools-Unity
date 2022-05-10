
namespace StardropTools.Tween
{
    [UnityEngine.RequireComponent(typeof(TweenComponentColor))]
    public class TweenComponentImageColor : TweenComponentImage
    {
        [UnityEngine.Space]
        public TweenComponentColor tween;

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

        void UpdateValue(UnityEngine.Color value) => target.color = value;

        protected void OnValidate()
        {
            if (tween == null)
                tween = GetComponent<TweenComponentColor>();

            tween.tweenData = data;
        }
    }
}