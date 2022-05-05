using System.Collections;
using UnityEngine;

namespace StardropTools.UI
{
    public class UITweenAnimation : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] protected RectTransformObject rectObject;
        [SerializeField] protected bool open;
        [SerializeField] protected bool close;
        [Space]
        [SerializeField] protected Transform tweenClusterParent;
        [SerializeField] protected TweenCluster[] tweenClusters;
        [SerializeField] protected bool getClusters;

        public readonly CoreEvent OnOpen = new CoreEvent();
        public readonly CoreEvent OnClose = new CoreEvent();

        public virtual void Open()
        {
            rectObject.SetActive(true);

            if (tweenClusters != null && tweenClusters.Length > 0)
                foreach (TweenCluster cluster in tweenClusters)
                    cluster.InitializeTweens();

            OnOpen?.Invoke();
        }

        public virtual void Close()
        {
            if (rectObject.IsActive)
                rectObject.SetActive(false);

            OnClose?.Invoke();
        }

        void GetTweenClusters()
        {
            tweenClusters = Utilities.GetItems<TweenCluster>(tweenClusterParent);

            if (tweenClusters != null && tweenClusters.Length > 0)
                foreach (TweenCluster cluster in tweenClusters)
                    cluster.GetSequenceData();
        }

        protected void OnValidate()
        {
            if (getClusters)
                GetTweenClusters();
            getClusters = false;
        }
    }
}