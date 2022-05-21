﻿

namespace StardropTools.Tween
{
    public abstract class TweenComponent : UnityEngine.MonoBehaviour, ITweenComponent
    {
        public enum Initalization { none, Start }

        public bool testTween;
        [UnityEngine.Space]
        public int tweenID;
        public Initalization initalization;
        public TweenBase tween;
        public TweenComponentData data;

        public Tween.LoopType Loop { get => data.loop; set => data.loop = value; }
        public Tween.EaseCurve Ease { get => data.ease; set => data.ease = value; }

        public float Duration { get => data.duration; set => data.duration = value; }
        public float Delay { get => data.delay; set => data.delay = value; }

        public bool IgnoreTimeScale { get => data.ignoreTimeScale; set => data.ignoreTimeScale = value; }
        public bool HasStart { get => data.hasStart; set => data.hasStart = value; }


        public CoreEvent OnStart { get => tween.OnStart; }
        public CoreEvent OnComplete { get => tween.OnComplete; }
        public CoreEvent OnPaused { get => tween.OnPaused; }
        public CoreEvent OnCanceled { get => tween.OnCanceled; }

        public CoreEvent OnDelayStart { get => tween.OnDelayStart; }
        public CoreEvent OnDelayComplete { get => tween.OnDelayComplete; }


        private void Start()
        {
            if (initalization == Initalization.Start)
                InitializeTween();
        }

        public abstract void InitializeTween();

        public virtual void PauseTween()
        {
            if (tween != null)
                tween.Pause();
        }

        public virtual void StopTween()
        {
            if (tween != null)
                tween.Stop();
        }

        public virtual void SetTweenID(int value)
        {
            tweenID = value;

            if (UnityEngine.Application.isPlaying)
                tween.tweenID = value;
        }

        protected virtual void OnValidate()
        {
            if (testTween)
                InitializeTween();
            testTween = false;
        }
    }
}