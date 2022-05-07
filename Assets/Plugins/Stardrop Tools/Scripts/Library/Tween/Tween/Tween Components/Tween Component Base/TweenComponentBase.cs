
namespace StardropTools.Tween
{
    public class TweenComponentBase : UnityEngine.MonoBehaviour
    {
        public int tweenID;
        public Tween.TweenType tweenType;
        public Tween.LoopType loop;
        public Tween.EaseCurve ease;
        [UnityEngine.Space]
        public float duration = .15f;
        public float delay;
        public bool hasStart = true;
        [UnityEngine.SerializeField] protected bool copyStartValues = true;

        protected virtual void OnValidate()
        {

        }
    }
}