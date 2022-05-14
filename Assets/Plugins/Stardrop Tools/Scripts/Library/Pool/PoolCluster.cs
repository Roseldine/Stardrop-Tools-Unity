
using UnityEngine;

namespace StardropTools.Pool
{
    public class PoolCluster : MonoBehaviour
    {
        [SerializeField] ClusterData clusterData;
        [SerializeField] GameObjectPool[] pools;

        bool isInitialized;

        public string ClusterName { get => clusterData.clusterName; set => clusterData.clusterName = value; }
        public int ClusterID { get => clusterData.clusterID; set => clusterData.clusterID = value; }


        public void Initialize(int clusterIndex)
        {
            if (isInitialized)
                return;

            clusterData.clusterID = clusterIndex;
            var self = transform;
            for (int i = 0; i < pools.Length; i++)
                pools[i].Populate(clusterData, i, self);

            isInitialized = true;
        }

        public PooledObject SpawnFromPool(int poolIndex, Vector3 position, Quaternion rotation, Transform parent)
            => pools[poolIndex].Spawn(position, rotation, parent);

        public T SpawnFromPool<T>(int poolIndex, Vector3 position, Quaternion rotation, Transform parent)
            => pools[poolIndex].Spawn<T>(position, rotation, parent);

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


        private void OnValidate()
        {
            if (pools.Exists())
                for (int i = 0; i < pools.Length; i++)
                {
                    pools[i].ClusterID = ClusterID;
                    pools[i].ClusterName = ClusterName;
                    pools[i].PoolID = i;
                    pools[i].PoolName = pools[i].name;

                    if (pools[i].name.Length == 0 && pools[i].Prefab != null)
                        pools[i].name = pools[i].Prefab.name;
                }
        }
    }
}