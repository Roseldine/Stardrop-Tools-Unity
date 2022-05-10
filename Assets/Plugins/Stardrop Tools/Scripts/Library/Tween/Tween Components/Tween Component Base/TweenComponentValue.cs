
namespace StardropTools.Tween
{
    public abstract class TweenComponentValue : UnityEngine.MonoBehaviour, ITweenComponent
    {
        public int tweenID;
        public TweenComponentBaseData tweenData;
        [UnityEngine.SerializeField] protected bool copyStartValues = true;

        #region Properties
        public Tween.LoopType Loop { get => tweenData.loop; set => tweenData.loop = value; }
        public Tween.EaseCurve Ease { get => tweenData.ease; set => tweenData.ease = value; }

        public float Duration { get => tweenData.duration; set => tweenData.duration = value; }
        public float Delay { get => tweenData.delay; set => tweenData.delay = value; }

        public bool IgnoreTimeScale { get => tweenData.ignoreTimeScale; set => tweenData.ignoreTimeScale = value; }
        public bool HasStart { get => tweenData.hasStart; set => tweenData.hasStart = value; }
        #endregion // properties

        protected UnityEngine.AnimationCurve curve;
        protected TweenBase tween;

        public virtual void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);
        }

        public void PauseTween() => tween.Pause();
        public void StopTween() => tween.Stop();

        protected virtual void OnValidate() { }
    }
}