
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenInt : TweenBase
    {
        protected int start;
        protected int target;
        protected int lerped;

        public static CoreEvent<int> OnUpdate = new CoreEvent<int>();

        public TweenInt(Tween.TweenTarget type, int tweenID, int startFloat, int targetFloat, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<int> updateEvent)
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
            lerped = (int)Mathf.LerpUnclamped(start, target, curve.Evaluate(percent));
            OnUpdate?.Invoke(lerped);
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }

        protected override void PingPong()
        {
            int temp = start;
            start = target;
            target = temp;

            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }
    }
}