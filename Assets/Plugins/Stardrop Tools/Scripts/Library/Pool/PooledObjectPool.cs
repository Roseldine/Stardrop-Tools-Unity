

namespace StardropTools.Pool
{
    [System.Serializable]
    public class PooledObjectPool : IPool
    {
        public string name;
        public PoolData poolData;
        public PoolData clusterData;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] UnityEngine.GameObject prefab;
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

        System.Collections.Generic.Queue<PooledObject> queue = new System.Collections.Generic.Queue<PooledObject>();
        System.Collections.Generic.List<PooledObject> cache;

        UnityEngine.Transform parent;
        bool isInitialized;

        #region Properties
        public string ClusterName { get => clusterData.name; set => clusterData.name = value; }
        public int ClusterID { get => clusterData.id; set => clusterData.id = value; }

        public string PoolName { get => poolData.name; set => poolData.name = value; }
        public int PoolID { get => poolData.id; set => poolData.id = value; }

        public UnityEngine.GameObject Prefab { get => prefab; }
        #endregion // properties


        #region Poolable Lifetime Cache
        public System.Collections.Generic.Dictionary<float, UnityEngine.WaitForSeconds> poolableLifetimeDictionary = new System.Collections.Generic.Dictionary<float, UnityEngine.WaitForSeconds>();
        public UnityEngine.WaitForSeconds GetWait(float time)
        {
            if (poolableLifetimeDictionary.TryGetValue(time, out var wait)) return wait;

            poolableLifetimeDictionary[time] = new UnityEngine.WaitForSeconds(time);
            return poolableLifetimeDictionary[time];
        }
        #endregion // poolable lifetime cache


        public PooledObjectPool(PoolData clusterData, PoolData poolData, UnityEngine.GameObject prefab, int capacity, bool overflow = false, UnityEngine.Transform parent = null, bool populate = true)
        {
            this.clusterData = clusterData;
            this.poolData = poolData;

            this.prefab = prefab;
            this.capacity = capacity;
            this.overflow = overflow;
            this.parent = parent;

            if (populate)
                Populate(parent);
        }

        public void Populate(PoolData clusterData, int poolIndex, UnityEngine.Transform parent)
        {
            this.clusterData = clusterData;
            poolData.id = poolIndex;

            Populate(parent);
        }

        public void Populate(UnityEngine.Transform parent)
        {
            if (isInitialized)
                return;

            if (capacity == 0)
            {
                UnityEngine.Debug.Log("Can't Populate! Pool Capacity is ZERO!...");
                return;
            }

            this.parent = parent;

            queue = new System.Collections.Generic.Queue<PooledObject>();
            cache = new System.Collections.Generic.List<PooledObject>();

            poolableLifetimeDictionary = new System.Collections.Generic.Dictionary<float, UnityEngine.WaitForSeconds>();

            for (int i = 0; i < capacity; i++)
            {
                UnityEngine.GameObject baseObject = UnityEngine.Object.Instantiate(prefab, parent);
                PooledObject pooled = baseObject.GetComponent<PooledObject>();

                // add pooled component if it doesn't have any
                if (pooled == null)
                {
                    pooled = baseObject.AddComponent<PooledObject>();

                    pooled.name += " - " + i;
                    pooled.Initialize(this, i, false);
                }
                
                // update pool info
                else
                {
                    pooled.name += " - " + i;
                    pooled.Initialize(this, i, false);
                }

                queue.Enqueue(pooled);
            }

            isInitialized = true;
        }

        public PooledObject Spawn(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Transform parent)
        {
            if (capacity == 0)
            {
                UnityEngine.Debug.Log("Can't Spawn! Pool Capacity is ZERO!...");
                return null;
            }

            if (active >= capacity && overflow)
            {
                PooledObject pooled = UnityEngine.Object.Instantiate(prefab, parent).AddComponent<PooledObject>();

                // set Transforms
                pooled.SetPosition(position);
                pooled.SetRotation(rotation);
                pooled.SetParent(parent);

                pooled.Position = position;

                pooled.SetActive(true);

                pooled.OnSpawn();

                // Add support for async task
                if (lifeTime > 0)
                    pooled.LifeTime(this, lifeTime);

                if (cache.Contains(pooled) == false)
                    cache.Add(pooled);

                overflowCount++;
                return pooled;
            }

            else
            {
                if (queue.Count == 0)
                {
                    UnityEngine.Debug.Log("Can't Spawn! Pool Capacity is ZERO!...");
                    return null;
                }

                // dequeue (get from queue)
                PooledObject pooled = queue.Dequeue();

                // make sure we are spawning a disabled item
                if (active != capacity)
                {
                    int iterations = 0;

                    while (pooled.IsActive)
                    {
                        queue.Enqueue(pooled);
                        pooled = queue.Dequeue();

                        // make sure while loop doesnt become infinite
                        iterations++;
                        if (iterations > active || iterations == capacity)
                            break;
                    }
                }

                // set Transforms
                pooled.SetPosition(position);
                pooled.SetRotation(rotation);
                pooled.SetParent(parent);

                // activate & place on queue/list again
                pooled.SetActive(true);
                queue.Enqueue(pooled);

                pooled.OnSpawn();

                if (lifeTime > 0)
                    pooled.LifeTime(this, lifeTime);

                if (cache.Contains(pooled) == false)
                    cache.Add(pooled);

                active++;
                return pooled;
            }
        }

        public T Spawn<T>(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Transform parent)
        {
            if (capacity == 0)
            {
                UnityEngine.Debug.Log("Can't Spawn! Pool Capacity is ZERO!...");
                return default;
            }

            PooledObject pooledObj = Spawn(position, rotation, parent);
            T component = pooledObj.GetComponent<T>();
            return component;
        }



        public void Despawn(PooledObject pooled, bool resetParent = true)
        {
            if (pooled != null && pooled.PoolHash == GetHashCode())
            {
                if (resetParent)
                    pooled.SetParent(parent);

                pooled.OnDespawn();
                pooled.SetActive(false);
                queue.Enqueue(pooled);

                active--;
            }

            else
                UnityEngine.Debug.LogWarning("Object didn't come from this pool!");
        }

        public void Despawn(UnityEngine.GameObject pooledObject, bool resetParent = true)
        {
            // find pooled equivalent
            for (int i = 0; i < cache.Count; i++)
                if (cache[i].GameObject.Equals(pooledObject))
                {
                    Despawn(cache[i], resetParent);
                    break;
                }
        }

        public void DespawnAll()
        {
            if (cache.Exists())
                for (int i = 0; i < cache.Count; i++)
                    Despawn(cache[i]);

            if (overflowCount > 0)
                for (int i = 0; i < overflowCount; i++)
                {
                    var pooled = queue.Dequeue();
                    UnityEngine.Object.Destroy(pooled.GameObject);
                }
        }
    }
}
