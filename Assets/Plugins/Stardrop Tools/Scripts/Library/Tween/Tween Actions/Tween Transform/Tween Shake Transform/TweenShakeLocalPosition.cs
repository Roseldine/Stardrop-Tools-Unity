
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeLocalPosition : TweenShakeVector3
    {
        Transform transform;

        public TweenShakeLocalPosition(Transform transform, int tweenID, Vector3 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector3> updateEvent = null)
                      : base(tweenID, transform.localPosition, intensity, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            this.transform = transform;
            start = transform.localPosition;

            SetBaseValues(Tween.TweenType.ShakeLocalPosition, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }
        

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            transform.localPosition = lerped;
        }
    }
    
}