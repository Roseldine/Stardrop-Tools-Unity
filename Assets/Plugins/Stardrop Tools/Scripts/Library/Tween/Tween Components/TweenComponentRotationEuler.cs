
namespace StardropTools.Tween
{
    [UnityEngine.RequireComponent(typeof(TweenComponentVector3))]
    public class TweenComponentRotationEuler : TweenComponentTransform
    {
        [UnityEngine.Space]
        public TweenComponentVector3 tween;

        public override void InitializeTween()
        {
            tween.OnTween.AddListener(UpdateValue);
            tween.InitializeTween();
        }

        public override void PauseTween() => tween.PauseTween();
        public override void CancelTween() => tween.CancelTween();

        public override void SetTweenID(int value)
        {
            base.SetTweenID(value);
            tween.tweenID = value;
            tween.tweenData = data;
        }

        void UpdateValue(UnityEngine.Vector3 value)
        {
            if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                target.eulerAngles = value;
            else
                target.localEulerAngles = value;
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
                    tween.startValue = target.eulerAngles;
                    tween.targetValue = target.eulerAngles;
                }
                else
                {
                    tween.startValue = target.localEulerAngles;
                    tween.targetValue = target.localEulerAngles;
                }

                copyValues = false;
            }
        }
    }
}