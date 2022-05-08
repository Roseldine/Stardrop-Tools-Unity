
namespace StardropTools.Tween
{
    [UnityEngine.RequireComponent(typeof(TweenComponentVector3))]
    public class TweenComponentLocalScale : TweenComponentTransform
    {
        [UnityEngine.Space]
        public TweenComponentVector3 tween;

        public override void InitializeTween()
        {
            tween.OnTween.AddListener(UpdateValue);
            tween.InitializeTween();
        }

        public override void SetTweenID(int value)
        {
            base.SetTweenID(value);
            tween.tweenID = value;
            tween.tweenData = data;
        }

        public override void PauseTween() => tween.PauseTween();
        public override void CancelTween() => tween.CancelTween();

        void UpdateValue(UnityEngine.Vector3 value) => target.localScale = value;

        protected void OnValidate()
        {
            if (tween == null)
                tween = GetComponent<TweenComponentVector3>();

            tween.tweenData = data;

            if (copyValues)
            {
                tween.startValue = target.localScale;
                tween.targetValue = target.localScale;

                copyValues = false;
            }
        }
    }
}