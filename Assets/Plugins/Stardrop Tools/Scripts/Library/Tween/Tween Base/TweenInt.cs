
using UnityEngine;

namespace StardropTools.Tween
{
    [System.Serializable]
    public class TweenInt : TweenBase
    {
        public int start;
        public int target;
        public int lerped;

        public static BaseEvent<int> OnUpdate = new BaseEvent<int>();

        public TweenInt(int tweenID, int startInt, int targetInt, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, BaseEvent<int> updateEvent)
        {
            start = startInt;
            target = targetInt;

            SetBaseValues(Tween.TweenType.Integer, tweenID, duration, delay, ignoreTimeScale, curve, loop);

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