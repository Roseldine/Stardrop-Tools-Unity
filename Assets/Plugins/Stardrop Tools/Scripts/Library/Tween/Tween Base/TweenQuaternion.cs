
using UnityEngine;

namespace StardropTools.Tween
{
    [System.Serializable]
    public class TweenQuaternion : TweenBase
    {
        protected Quaternion start;
        protected Quaternion target;
        protected Quaternion lerped;

        public static BaseEvent<Quaternion> OnUpdate = new BaseEvent<Quaternion>();

        public TweenQuaternion(int tweenID, Quaternion startVector, Quaternion targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, BaseEvent<Quaternion> updateEvent = null)
        {
            start = startVector;
            target = targetVector;

            SetBaseValues(Tween.TweenType.Quaternion, tweenID, duration, delay, ignoreTimeScale, curve, loop);

            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;

            OnUpdate = updateEvent;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Quaternion.SlerpUnclamped(start, target, curve.Evaluate(percent));
            OnUpdate?.Invoke(lerped);
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }

        protected override void PingPong()
        {
            Quaternion temp = start;
            start = target;
            target = temp;

            ResetRuntime();
            ChangeState(Tween.TweenState.running);
        }
    }
}