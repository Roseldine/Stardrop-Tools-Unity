using System.Collections;
using UnityEngine;
using StardropTools.TweenSurge;

namespace StardropTools.UI
{
    public class UITweenSurgeAnimation : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] protected CoreRectTransform rectObject;
        [SerializeField] protected bool open;
        [SerializeField] protected bool close;
        [Space]
        [SerializeField] protected Transform tweenClusterParent;
        [SerializeField] protected TweenSurgeCluster[] tweenClusters;
        [SerializeField] protected bool getClusters;

        public readonly CoreEvent OnOpen = new CoreEvent();
        public readonly CoreEvent OnClose = new CoreEvent();

        public virtual void Open()
        {
            rectObject.SetActive(true);

            if (tweenClusters != null && tweenClusters.Length > 0)
                foreach (TweenSurgeCluster cluster in tweenClusters)
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
            tweenClusters = Utilities.GetItems<TweenSurgeCluster>(tweenClusterParent);

            if (tweenClusters != null && tweenClusters.Length > 0)
                foreach (TweenSurgeCluster cluster in tweenClusters)
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