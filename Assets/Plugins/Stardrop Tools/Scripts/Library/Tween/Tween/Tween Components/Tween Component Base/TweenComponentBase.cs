
namespace StardropTools.Tween
{
    public abstract class TweenComponentBase : UnityEngine.MonoBehaviour
    {
        public int tweenID;
        [UnityEngine.SerializeField] TweenComponentBaseData tweenData;
        [UnityEngine.SerializeField] protected bool copyStartValues = true;

        #region Properties
        public TweenComponentBaseData.Initalization initalization { get => tweenData.initalization; set => tweenData.initalization = value; }

        public Tween.LoopType Loop { get => tweenData.loop; set => tweenData.loop = value; }
        public Tween.EaseCurve Ease { get => tweenData.ease; set => tweenData.ease = value; }

        public float Duration { get => tweenData.duration; set => tweenData.duration = value; }
        public float Delay { get => tweenData.delay; set => tweenData.delay = value; }

        public bool IgnoreTimeScale { get => tweenData.ignoreTimeScale; set => tweenData.ignoreTimeScale = value; }
        public bool HasStart { get => tweenData.hasStart; set => tweenData.hasStart = value; }
        #endregion // properties

        protected UnityEngine.AnimationCurve curve;
        protected TweenBase tween;

        private void Awake()
        {
            if (initalization == TweenComponentBaseData.Initalization.Awake)
                InitializeTween();
        }

        private void Start()
        {
            if (initalization == TweenComponentBaseData.Initalization.Start)
                InitializeTween();
        }

        public virtual void InitializeTween()
        {
            curve = Tween.GetEaseCurve(Ease);
        }

        public void PauseTween() => tween.Pause();
        public void StopTween() => tween.Cancel();

        protected virtual void OnValidate() { }
    }
}