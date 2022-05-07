
namespace StardropTools.TweenSurge
{
    public class TweenSurgeActionBase : UnityEngine.MonoBehaviour
    {
        public enum TweenType { none, position, rotation, localScale, rectSize, colorImage, colorSprite, colorMaterial }

        //public TweenType type;
        public bool copyValues;
        public bool setName;
        public bool hasStart;
        [UnityEngine.Space]
        public int index;
        public Pixelplacement.Tween.TweenType type;
        public Pixelplacement.Tween.LoopType loop;
        public UnityEngine.AnimationCurve curve;
        [UnityEngine.Space]
        public float duration = .15f;
        public float delay;

        public virtual void Initialize() { }
        protected void SetName(string agentName) => name = "TA - " + type.ToString() + " - " + agentName;


        public virtual void OnValidateMethods() { }

        protected void OnValidate()
        {
            OnValidateMethods();
        }
    }
}