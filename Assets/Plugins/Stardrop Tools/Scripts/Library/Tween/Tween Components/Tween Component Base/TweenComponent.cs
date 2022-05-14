

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