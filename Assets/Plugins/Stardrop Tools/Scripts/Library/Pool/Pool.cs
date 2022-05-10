

namespace StardropTools.Pool
{
    [System.Serializable]
    public class Pool<T> where T : class, IPool
    {
        [UnityEngine.SerializeField] PoolData poolData;
        [UnityEngine.SerializeField] ClusterData clusterData;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] T prefab;
        [UnityEngine.SerializeField] int capacity;
        /// <summary>
        /// Can pool spawn more than amount?
        /// </summary>
        [UnityEngine.SerializeField] bool overflow;
        [UnityEngine.SerializeField] int overflowCount;
        [UnityEngine.Space]
        /// <summary>
        /// if != 0, despawn after lifetime is reached
        /// </summary>
        [UnityEngine.Min(0)][UnityEngine.SerializeField] float lifeTime;
        [UnityEngine.SerializeField] int active;

        System.Collections.Generic.Queue<T> queue;
        System.Collections.Generic.List<T> cache;

        /// <summary>
        /// Gets filled up Spawn calls are made
        /// </summary>
        System.Collections.Generic.List<UnityEngine.MonoBehaviour> behaviours;
        UnityEngine.Transform parent;
        bool isInitialized;

        #region Properties
        public string ClusterName { get => clusterData.clusterName; set => clusterData.clusterName = value; }
        public int ClusterID { get => clusterData.clusterID; set => clusterData.clusterID = value; }

        public string PoolName { get => poolData.poolName; set => poolData.poolName = value; }
        public int PoolID { get => poolData.poolID; set => poolData.poolID = value; }
        #endregion // properties


        public Pool(ClusterData clusterData, PoolData poolData, T prefab, int capacity, bool overflow, bool populate = true)
        {
            this.clusterData = clusterData;
            this.poolData = poolData;

            this.prefab = prefab;
            this.capacity = capacity;
            this.overflow = overflow;

            if (populate)
                Populate();
        }

        public void Populate(ClusterData clusterData, int poolIndex)
        {
            this.clusterData = clusterData;
            PoolID = poolIndex;

            Populate();
        }

        public void Populate()
        {
            if (isInitialized)
                return;

            queue = new System.Collections.Generic.Queue<T>();
            cache = new System.Collections.Generic.List<T>();
            behaviours = new System.Collections.Generic.List<UnityEngine.MonoBehaviour>();

            for (int i = 0; i < capacity; i++)
            {
                T pooled = System.Activator.CreateInstance<T>();
                queue.Enqueue(pooled);
            }

            isInitialized = true;
        }

        public T Spawn()
        {
            if (active >= capacity && overflow)
            {
                T pooled = System.Activator.CreateInstance<T>();

                if (cache.Contains(pooled) == false)
                    cache.Add(pooled);

                overflowCount++;
                return pooled;
            }

            else
            {
                // dequeue (get from queue)
                T pooled = queue.Dequeue();

                if (cache.Contains(pooled) == false)
                    cache.Add(pooled);

                active++;
                return pooled;
            }
        }



        public void Despawn(T pooled)
        {
            // find pooled equivalent
            for (int i = 0; i < cache.Count; i++)
                if (cache[i].Equals(pooled))
                {
                    queue.Enqueue(pooled);
                    break;
                }

             UnityEngine.Debug.LogWarning("Object didn't come from this pool!");
        }

        public void DespawnAll()
        {
            if (cache.Exists())
                for (int i = 0; i < cache.Count; i++)
                    Despawn(cache[i]);

            if (overflowCount > 0)
                for (int i = 0; i < overflowCount; i++)
                    queue.Dequeue();
        }
    }
}
