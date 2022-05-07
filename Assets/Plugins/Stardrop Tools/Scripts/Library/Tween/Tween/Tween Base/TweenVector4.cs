
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenVector4 : TweenBase
    {
        protected Vector4 start;
        protected Vector4 target;
        protected Vector4 lerped;

        public static CoreEvent<Vector4> OnUpdate = new CoreEvent<Vector4>();

        public TweenVector4(Tween.TweenTarget type, int tweenID, Vector4 startFloat, Vector4 targetFloat, CoreEvent<Vector4> updateEvent, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop)
        {
            start = startFloat;
            target = targetFloat;

            SetBaseValues(type, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Vector4.LerpUnclamped(start, target, curve.Evaluate(percent));
            OnUpdate?.Invoke(lerped);
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }

        protected override void PingPong()
        {
            Vector4 temp = start;
            start = target;
            target = temp;

            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }
    }
}