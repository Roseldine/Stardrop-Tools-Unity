﻿
namespace StardropTools.Tween
{
    public class TweenComponentShakePosition : TweenComponentTransform
    {
        [UnityEngine.Space]
        public UnityEngine.Vector3 intensity = UnityEngine.Vector3.one;
        public UnityEngine.Vector3 end = UnityEngine.Vector3.one;

        public BaseEvent<UnityEngine.Vector3> OnTween = new BaseEvent<UnityEngine.Vector3>();

        public override void InitializeTween()
        {
            if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                tween = Tween.ShakePosition(target, intensity, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
            else
                tween = Tween.ShakeLocalPosition(target, intensity, end, data.duration, data.delay, data.ignoreTimeScale, data.curve, data.loop, target.GetInstanceID(), OnTween);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (copyValues)
            {
                if (globalOrLocal == ITweenComponent.GlobalOrLocal.global)
                    end = target.position;
                else
                    end = target.localPosition;

                copyValues = false;
            }
        }
    }
}