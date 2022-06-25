
using UnityEngine;

namespace StardropTools.UI
{
    public abstract class UIToggleComponentWithAnimation : UIToggleComponent
    {
        [Header("Animation")]
        [SerializeField] protected AnimationCurve animCurve;
        [SerializeField] protected float animTime = .2f;

        protected Coroutine animCR;

        protected void StopAnimCR()
        {
            if (animCR != null)
                StopCoroutine(animCR);
        }
    }
}


