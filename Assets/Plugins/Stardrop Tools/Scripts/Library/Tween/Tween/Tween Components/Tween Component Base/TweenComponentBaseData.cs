
namespace StardropTools.Tween
{
    [System.Serializable]
    public class TweenComponentBaseData
    {
        public enum Initalization { none, Awake, Start }

        public Initalization initalization;
        public Tween.LoopType loop;
        public Tween.EaseCurve ease;
        [UnityEngine.Space]
        public float duration = 1;
        public float delay;
        [UnityEngine.Space]
        public bool ignoreTimeScale = true;
        public bool hasStart = true;
    }
}