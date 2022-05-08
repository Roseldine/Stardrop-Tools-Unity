

namespace StardropTools.Tween
{
    public abstract class TweenComponentMaterial : TweenComponent
    {
        public UnityEngine.Material target;
        [UnityEngine.SerializeField] protected bool copyValues;
    }
}