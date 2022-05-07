using System.Collections;
using UnityEngine;

namespace StardropTools.Pool
{
    [System.Serializable]
    public class PoolComponent<T> where T : Component
    {
        public int id;
        public T component;
    }
}