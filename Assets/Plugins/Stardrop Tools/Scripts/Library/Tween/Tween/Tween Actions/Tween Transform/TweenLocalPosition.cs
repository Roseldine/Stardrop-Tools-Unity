﻿
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenLocalPosition : TweenVector3
    {
        Transform transform;

        public TweenLocalPosition(Transform transform, int tweenID, Vector3 targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector3> updateEvent = null)
                      : base(tweenID, targetVector, targetVector, duration, delay, ignoreTimeScale, curve, loop, updateEvent = null)
        {
            this.transform = transform;
            start = transform.localPosition; // local
            target = targetVector;

            SetBaseValues(Tween.TweenType.LocalPosition, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }
        

        protected override void TweenUpdate(float percent)
        {
            lerped = Vector3.LerpUnclamped(start, target, curve.Evaluate(percent));
            transform.localPosition = lerped;

            OnUpdate?.Invoke(lerped);
        }
    }
    
}