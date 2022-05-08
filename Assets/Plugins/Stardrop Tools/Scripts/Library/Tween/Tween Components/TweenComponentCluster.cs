using System.Collections;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenComponentCluster : MonoBehaviour
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
                tweens[i].CancelTween();
        }

        void GetTweens()
        {
            System.Collections.Generic.List<TweenComponentValue> list = new System.Collections.Generic.List<TweenComponentValue>();

            for (int p = 0; p < parentTweens.Length; p++)
            {
                var components = Utilities.GetItems<TweenComponentValue>(parentTweens[p]);
                list.AddArrayToList(components);
            }

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