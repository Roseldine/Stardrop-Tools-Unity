﻿
using UnityEngine;

namespace StardropTools.Pool
{
    public class PoolCluster : MonoBehaviour
    {
        [SerializeField] GameObject[] objectsToPool;
        [SerializeField] int objectsToPoolCapacity;
        [Space]
        [SerializeField] PoolData clusterData;
        [SerializeField] WeightedList<int> poolWeights;
        [SerializeField] bool copyWheightNumbers;
        [SerializeField] bool clearWheights;
        [SerializeField] bool clearEmpty;
        [SerializeField] GameObjectPool[] pools;

        bool isInitialized;

        public string ClusterName { get => clusterData.name; set => clusterData.name = value; }
        public int ClusterID { get => clusterData.id; set => clusterData.id = value; }
        public int PoolCount { get => pools.Length; }

        public void Initialize(int clusterIndex)
        {
            if (isInitialized)
                return;

            clusterData.id = clusterIndex;
            var self = transform;
            for (int i = 0; i < pools.Length; i++)
                pools[i].Populate(clusterData, i, self);

            isInitialized = true;
        }

        public PooledObject SpawnFromPool(int poolIndex, Vector3 position, Quaternion rotation, Transform parent)
            => pools[poolIndex].Spawn(position, rotation, parent);

        public T SpawnFromPool<T>(int poolIndex, Vector3 position, Quaternion rotation, Transform parent)
            => pools[poolIndex].Spawn<T>(position, rotation, parent);


        public PooledObject SpawnRandomFromPool(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (poolWeights.Count == 0)
                return pools[Random.Range(0, pools.Length)].Spawn(position, rotation, parent);
            else
                return pools[poolWeights.GetRandom()].Spawn(position, rotation, parent);
        }

        public T SpawnRandomFromPool<T>(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (poolWeights.Count == 0)
                return pools[Random.Range(0, pools.Length)].Spawn<T>(position, rotation, parent);
            else
                return pools[poolWeights.GetRandom()].Spawn<T>(position, rotation, parent);
        }


        public void DespawnFromPool(int poolIndex, PooledObject pooled, bool resetParent = true)
            => pools[poolIndex].Despawn(pooled, resetParent);

        public void DespawnFromPool(int poolIndex, GameObject pooledObject, bool resetParent = true)
            => pools[poolIndex].Despawn(pooledObject, resetParent);

        public void Despawn(PooledObject pooled, bool resetParent = true)
        {
            var pool = pools[pooled.PoolID];

            if (pool == null)
            {
                WrongDespawnMessage(pooled);
                return;
            }

            if (pool.PoolName == pooled.PoolName)
                pool.Despawn(pooled, resetParent);
            else
                WrongDespawnMessage(pooled, this);
        }

        public void DespawnAllFromPool(int poolIndex) => pools[poolIndex].DespawnAll();

        public void DespawnAllPools()
        {
            foreach (GameObjectPool pool in pools)
                pool.DespawnAll();
        }

        void WrongDespawnMessage(PooledObject pooled)
            => Debug.LogFormat("<color=yellow>Attempted to despawn object {0} of pool id: {1}, not spawned from cluster id: {2}</color>", pooled.name, pooled.PoolID, pooled.ClusterID);


        void WrongDespawnMessage(PooledObject pooled, PoolCluster cluster)
            => Debug.LogFormat("<color=yellow>Attempted to despawn object {0} of pool id: {1}, not spawned from cluster id: {2}</color>", pooled.name, pooled.PoolID, cluster.ClusterID);

        public void RefreshPoolNames()
        {
            if (pools.Exists())
                for (int i = 0; i < pools.Length; i++)
                {
                    pools[i].clusterData = clusterData;

                    pools[i].poolData = new PoolData(i, pools[i].name);

                    if (pools[i] != null && pools[i].Prefab != null)
                        pools[i].name = pools[i].Prefab.name;
                }
        }

        protected void RefreshObjectToPool()
        {
            if (objectsToPool.Exists())
            {
                System.Collections.Generic.List<GameObject> newPoolObjects = new System.Collections.Generic.List<GameObject>();
                bool exists = false;

                // check if object pool already exists
                for (int i = 0; i < objectsToPool.Length; i++)
                {
                    exists = false;

                    for (int p = 0; p < pools.Length; p++)
                    {
                        if (pools[p].Prefab.Equals(objectsToPool[i]))
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (exists == false)
                        newPoolObjects.Add(objectsToPool[i]);
                }

                // no new objects to pool
                if (newPoolObjects.Exists() == false)
                {
                    objectsToPool = new GameObject[0];
                    Debug.Log("No NEW objects to pool");
                    return;
                }

                // make a copy of existing pools
                var list = new System.Collections.Generic.List<GameObjectPool>();
                for (int i = 0; i < pools.Length; i++)
                    list.Add(pools[i]);
                
                // create pools of objects in list
                for (int i = 0; i < newPoolObjects.Count; i++)
                {
                    PoolData poolData = new PoolData(newPoolObjects[i].name, i);
                    list.Add(new GameObjectPool(clusterData, poolData, newPoolObjects[i], objectsToPoolCapacity, false, null, false));
                    //Debug.Log("Pool prefab: " + pools[i].Prefab.name);
                }

                pools = list.ToArray();
                objectsToPool = new GameObject[0];
            }
        }

        private void OnValidate()
        {
            RefreshObjectToPool();
            //RefreshPoolNames();

            if (copyWheightNumbers)
            {
                poolWeights = new WeightedList<int>();
                for (int i = 0; i < pools.Length; i++)
                    poolWeights.Add(new WeightedItem<int>(i, pools.Length - i));

                copyWheightNumbers = false;
            }

            if (clearWheights)
            {
                poolWeights = new WeightedList<int>();
                clearWheights = false;
            }

            if (clearEmpty)
            {
                var poolList = new System.Collections.Generic.List<GameObjectPool>();

                for (int i = 0; i < pools.Length; i++)
                    if (pools[i].Prefab != null)
                        poolList.Add(pools[i]);

                pools = poolList.ToArray();

                clearEmpty = false;
            }
        }
    }
}