
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenVector2 : TweenBase
    {
        protected Vector2 start;
        protected Vector2 target;
        protected Vector2 lerped;

        public static CoreEvent<Vector2> OnUpdate = new CoreEvent<Vector2>();

        public TweenVector2(Tween.TweenTarget type, int tweenID, Vector2 startFloat, Vector2 targetFloat, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector2> updateEvent = null)
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
            lerped = Vector2.LerpUnclamped(start, target, curve.Evaluate(percent));
            OnUpdate?.Invoke(lerped);
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }

        protected override void PingPong()
        {
            Vector2 temp = start;
            start = target;
            target = temp;

            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }
    }
}