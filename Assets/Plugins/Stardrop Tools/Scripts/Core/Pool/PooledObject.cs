
using UnityEngine;

namespace StardropTools.Pool
{
    public class PooledObject : TransformObject, IPoolable
    {
        [Header("Pooled Object")]
        [SerializeField] int itemID;
        [Space]
        [SerializeField] string clusterName;
        [SerializeField] int clusterID;
        [Space]
        [SerializeField] string poolName;
        [SerializeField] int poolID;

        Coroutine lifetimeCR;

        public string ClusterName { get => clusterName; set => clusterName = value; }
        public int ClusterID { get => clusterID; set => clusterID = value; }

        public string PoolName { get => poolName; set => poolName = value; }
        public int PoolID { get => poolID; set => poolID = value; }

        public int ItemID { get => itemID; set => itemID = value; }

        public void Initialize(int clusterID, string clusterName, int poolID, string poolName, int itemID, bool setActive = false)
        {
            this.clusterID = clusterID;
            this.clusterName = clusterName;
            this.poolID = poolID;
            this.poolName = poolName;
            itemID = itemID;

            SetActive(setActive);
            Initialize();
        }

        public virtual void OnSpawn() { }
        public virtual void OnDespawn()
        {
            if (lifetimeCR != null)
                StopCoroutine(lifetimeCR);
        }

        public virtual void LifeTime(Pool pool, float time)
            => lifetimeCR = StartCoroutine(LifetimeCR(pool, time));

        System.Collections.IEnumerator LifetimeCR(Pool pool, float time)
        {
            yield return pool.GetWait(time);
            pool.Despawn(this);
        }
    }
}
