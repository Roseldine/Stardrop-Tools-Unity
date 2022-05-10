
using System.Threading.Tasks;


namespace StardropTools.Pool
{
    public class PooledObject : CoreObject, IPoolable
    {
        [UnityEngine.Header("Pooled Object")]
        [UnityEngine.SerializeField] int itemID;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] ClusterData clusterData;
        [UnityEngine.SerializeField] PoolData poolData;

        UnityEngine.Coroutine lifetimeCR;

        public string ClusterName { get => clusterData.clusterName; set => clusterData.clusterName = value; }
        public int ClusterID { get => clusterData.clusterID; set => clusterData.clusterID = value; }

        public string PoolName { get => poolData.poolName; set => poolData.poolName = value; }
        public int PoolID { get => poolData.poolID; set => poolData.poolID = value; }

        public int ItemID { get => itemID; set => itemID = value; }

        public void Initialize(ClusterData clusterData, PoolData poolData, int itemID, bool setActive = false)
        {
            this.clusterData = clusterData;
            this.poolData = poolData;
            this.itemID = itemID;

            SetActive(setActive);
            Initialize();
        }

        public virtual void OnSpawn() { }
        public virtual void OnDespawn()
        {
            if (lifetimeCR != null)
                StopCoroutine(lifetimeCR);
        }

        public virtual void LifeTime(GameObjectPool pool, float time)
            => lifetimeCR = StartCoroutine(LifetimeCR(pool, time));

        System.Collections.IEnumerator LifetimeCR(GameObjectPool pool, float time)
        {
            yield return pool.GetWait(time);
            pool.Despawn(this);
        }

        public async void LifeTimeAsync(GameObjectPool pool, float time)
            => await LifetimeSync(pool, time);

        private async Task LifetimeSync(GameObjectPool pool, float time)
        {
            int milisenconds = MathUtility.ConvertToMiliseconds(time);
            await Task.Delay(milisenconds);
            pool.Despawn(this);
        }
    }
}
