

namespace StardropTools.Tween
{
    public abstract class TweenComponent : UnityEngine.MonoBehaviour, ITweenComponent
    {
        public enum Initalization { none, Start }

        public TweenComponentBaseData data;

        public int tweenID;
        public Initalization initalization;

        private void Start()
        {
            if (initalization == Initalization.Start)
                InitializeTween();
        }

        public abstract void InitializeTween();
        public abstract void PauseTween();
        public abstract void CancelTween();

        public virtual void SetTweenID(int value)
        {
            tweenID = value;
        }
    }
}