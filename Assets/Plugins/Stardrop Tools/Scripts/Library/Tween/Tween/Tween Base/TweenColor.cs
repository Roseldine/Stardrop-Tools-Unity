﻿
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenColor : TweenBase
    {
        protected Color start;
        protected Color target;
        protected Color lerped;

        public static CoreEvent<Color> OnUpdate = new CoreEvent<Color>();

        public TweenColor(Tween.TweenTarget type, int tweenID, Color startColor, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Color> updateEvent = null)
        {
            start = startColor;
            target = targetColor;

            SetBaseValues(type, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Color.LerpUnclamped(start, target, curve.Evaluate(percent));
            OnUpdate?.Invoke(lerped);
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }

        protected override void PingPong()
        {
            Color temp = start;
            start = target;
            target = temp;

            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }
    }
}