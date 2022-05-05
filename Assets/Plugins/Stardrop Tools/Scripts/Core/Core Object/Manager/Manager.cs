
using UnityEngine;

namespace StardropTools
{
    public abstract class Manager : CoreObject
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public void InitializeManager()
        {
            Initialize();
        }

        protected T[] GetItems<T>(Transform parent)
            => Utilities.GetItems<T>(parent);
    }
}