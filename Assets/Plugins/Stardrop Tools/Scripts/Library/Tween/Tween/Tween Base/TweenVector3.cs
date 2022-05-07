
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenVector3 : TweenBase
    {
        protected Vector3 start;
        protected Vector3 target;
        protected Vector3 lerped;

        public static CoreEvent<Vector3> OnUpdate = new CoreEvent<Vector3>();

        public TweenVector3(Tween.TweenTarget type, int tweenID, Vector3 startFloat, Vector3 targetFloat, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, CoreEvent<Vector3> updateEvent = null)
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
            lerped = Vector3.LerpUnclamped(start, target, curve.Evaluate(percent));
            OnUpdate?.Invoke(lerped);
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }

        protected override void PingPong()
        {
            Vector3 temp = start;
            start = target;
            target = temp;

            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }
    }
}