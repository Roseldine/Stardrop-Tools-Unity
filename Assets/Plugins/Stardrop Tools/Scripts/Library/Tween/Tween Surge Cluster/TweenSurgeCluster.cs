
using UnityEngine;

namespace StardropTools.TweenSurge
{
    [AddComponentMenu("Stardrop / TweenSurge / Tween Cluster")]
    public class TweenSurgeCluster : MonoBehaviour
    {
        public int index;
        [SerializeField] float delay;
        [SerializeField] TweenSurgeActionBase[] tweenActions;
        [SerializeField] bool getData;

        public virtual void InitializeTweens()
        {
            if (delay > 0)
                Invoke(nameof(ValidateTweens), delay);
            else
                ValidateTweens();
        }

        void ValidateTweens()
        {
            if (tweenActions != null && tweenActions.Length > 0)
                foreach (var data in tweenActions)
                    TweenSurgeActionValidator.ValidateTween(data);
        }

        public void GetSequenceData()
        {
            if (tweenActions != null && tweenActions.Length != transform.childCount)
                tweenActions = Utilities.GetItems<TweenSurgeActionBase>(transform);

            if (tweenActions != null && tweenActions.Length > 0)
                for (int i = 0; i < tweenActions.Length; i++)
                    tweenActions[i].index = i;
        }

        private void OnValidate()
        {
            if (getData)
            {
                GetSequenceData();
                getData = false;
            }
        }
    }
}