
namespace StardropTools.Tween
{
    [UnityEngine.RequireComponent(typeof(TweenComponentVector3))]
    public class TweenComponentPosition : TweenComponentTransform
    {
        [UnityEngine.Space]
        public TweenComponentVector3 tween;

        public override void InitializeTween()
        {
            tween.InitializeTween();
            tween.OnTween.AddListener(UpdateValue);
        }

        public override void SetTweenID(int value)
        {
            base.SetTweenID(value);
            tween.tweenID = value;
            tween.tweenData = data;
        }

        public override void PauseTween() => tween.PauseTween();
        public override void StopTween() => tween.StopTween();

        void UpdateValue(UnityEngine.Vector3 value)
        {
            if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                target.position = value;
            else
                target.localPosition = value;
        }

        protected void OnValidate()
        {
            if (tween == null)
                tween = GetComponent<TweenComponentVector3>();

            tween.tweenData = data;

            if (copyValues)
            {
                if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                {
                    tween.startValue = target.position;
                    tween.targetValue = target.position;
                }
                else
                {
                    tween.startValue = target.localPosition;
                    tween.targetValue = target.localPosition;
                }

                copyValues = false;
            }
        }
    }
}