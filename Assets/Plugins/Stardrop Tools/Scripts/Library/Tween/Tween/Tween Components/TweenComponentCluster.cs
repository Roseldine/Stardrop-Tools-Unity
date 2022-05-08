using System.Collections;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenComponentCluster : MonoBehaviour
    {
        [SerializeField] Transform[] parentTweens;
        [SerializeField] TweenComponentBase[] tweens;
        [SerializeField] bool getTweens;

        void GetTweens()
        {
            System.Collections.Generic.List<TweenComponentBase> list = new System.Collections.Generic.List<TweenComponentBase>();

            for (int p = 0; p < parentTweens.Length; p++)
            {
                var components = Utilities.GetItems<TweenComponentBase>(parentTweens[p]);
                list.AddArrayToList(components);
            }

            if (tweens.Exists())
                for (int i = 0; i < tweens.Length; i++)
                    tweens[i].tweenID = i;
        }

        private void OnValidate()
        {
            if (getTweens)
                GetTweens();
            getTweens = false;
        }
    }
}