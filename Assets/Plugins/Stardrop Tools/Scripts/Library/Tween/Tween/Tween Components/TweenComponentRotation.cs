
namespace StardropTools.Tween
{
    [UnityEngine.RequireComponent(typeof(TweenComponentQuaternion))]
    public class TweenComponentRotation : TweenComponentTransform
    {
        [UnityEngine.Space]
        public TweenComponentQuaternion tween;

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

        void UpdateValue(UnityEngine.Quaternion value)
        {
            if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                target.rotation = value;
            else
                target.localRotation = value;
        }

        protected void OnValidate()
        {
            if (tween == null)
                tween = GetComponent<TweenComponentQuaternion>();

            tween.tweenData = data;

            if (copyValues)
            {
                if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                {
                    tween.startValue = target.rotation;
                    tween.targetValue = target.rotation;
                }
                else
                {
                    tween.startValue = target.localRotation;
                    tween.targetValue = target.localRotation;
                }

                copyValues = false;
            }
        }
    }
}