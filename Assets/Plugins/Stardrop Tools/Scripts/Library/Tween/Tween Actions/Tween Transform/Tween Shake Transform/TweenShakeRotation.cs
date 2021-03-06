
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeRotation : TweenShakeQuaternion
    {
        Transform transform;

        public TweenShakeRotation(Transform transform, int tweenID, Vector4 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, BaseEvent<Quaternion> updateEvent = null)
                      : base(tweenID, transform.rotation, intensity, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            start = transform.rotation;
            this.transform = transform;

            SetBaseValues(Tween.TweenType.Rotation, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }
        

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            transform.rotation = lerped;
        }
    }
    
}