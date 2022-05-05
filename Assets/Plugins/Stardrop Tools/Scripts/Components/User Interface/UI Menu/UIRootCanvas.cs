
using UnityEngine;

namespace StardropTools.UI
{
    [RequireComponent(typeof(UISizeCopy))]
    public class UIRootCanvas : RectTransformObject
    {
        [SerializeField] UISizeCopy sizeCopy;

        public override void Initialize()
        {
            base.Initialize();
            sizeCopy.Copy();
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (sizeCopy == null)
                sizeCopy = GetComponent<UISizeCopy>();
        }
    }
}
