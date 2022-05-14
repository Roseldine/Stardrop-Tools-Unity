﻿
namespace StardropTools.Tween
{
    public class TweenComponentRotationEuler : TweenComponentTransform
    {
        [UnityEngine.Space]
        public UnityEngine.Vector3 start = UnityEngine.Vector3.one;
        public UnityEngine.Vector3 end = UnityEngine.Vector3.one;

        public CoreEvent<UnityEngine.Vector3> OnTween = new CoreEvent<UnityEngine.Vector3>();

        public override void InitializeTween()
        {
            if (data.hasStart)
            {
                if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                    tween = Tween.RotationEuler(target, start, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
                else
                    tween = Tween.LocalRotationEuler(target, start, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            }

            else
            {
                if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                    tween = Tween.RotationEuler(target, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
                else
                    tween = Tween.LocalRotationEuler(target, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            }
        }
    }
}