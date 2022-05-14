﻿
namespace StardropTools.Tween
{
    public class TweenComponentShakeRotationEuler : TweenComponentTransform
    {
        [UnityEngine.Space]
        public UnityEngine.Vector3 intensity = UnityEngine.Vector3.one;

        public CoreEvent<UnityEngine.Vector3> OnTween = new CoreEvent<UnityEngine.Vector3>();

        public override void InitializeTween()
        {
            if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                tween = Tween.ShakeRotationEuler(target, intensity, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            else
                tween = Tween.ShakeLocalRotationEuler(target, intensity, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
        }
    }
}