
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenCluster : MonoBehaviour
    {
        [SerializeField] Transform[] parentTweens;
        [SerializeField] TweenComponent[] tweens;
        [SerializeField] bool getTweens;

        public void InitializeTweens()
        {
            for (int i = 0; i < tweens.Length; i++)
                tweens[i].InitializeTween();
        }

        public void PauseTweens()
        {
            for (int i = 0; i < tweens.Length; i++)
                tweens[i].PauseTween();
        }

        public void StopTweens()
        {
            for (int i = 0; i < tweens.Length; i++)
                tweens[i].StopTween();
        }

        void GetTweens()
        {
            System.Collections.Generic.List<TweenComponent> list = new System.Collections.Generic.List<TweenComponent>();

            for (int p = 0; p < parentTweens.Length; p++)
            {
                var components = parentTweens[p].GetComponentsInChildren<TweenComponent>(); // Utilities.GetItems<TweenComponent>();
                list.AddArrayToList(components);
            }

            tweens = list.ToArray();

            if (tweens.Exists())
                for (int i = 0; i < tweens.Length; i++)
                    tweens[i].SetTweenID(i);
        }

        private void OnValidate()
        {
            if (getTweens)
                GetTweens();
            getTweens = false;
        }
    }
}