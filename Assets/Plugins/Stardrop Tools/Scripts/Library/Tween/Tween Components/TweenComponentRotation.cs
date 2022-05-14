
namespace StardropTools.Tween
{
    public class TweenComponentRotation : TweenComponentTransform
    {
        [UnityEngine.Space]
        public UnityEngine.Quaternion start;
        public UnityEngine.Quaternion end;

        public CoreEvent<UnityEngine.Quaternion> OnTween = new CoreEvent<UnityEngine.Quaternion>();


        public override void InitializeTween()
        {
            if (data.hasStart)
            {
                if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                    tween = Tween.Rotation(target, start, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
                else
                    tween = Tween.LocalRotation(target, start, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            }

            else
            {
                if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                    tween = Tween.Rotation(target, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
                else
                    tween = Tween.LocalRotation(target, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            }
        }
    }
}