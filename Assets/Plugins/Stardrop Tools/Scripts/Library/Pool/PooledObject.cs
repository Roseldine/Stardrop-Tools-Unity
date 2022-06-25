
using System.Threading.Tasks;


namespace StardropTools.Pool
{
    public class PooledObject : BaseObject, IPoolable
    {
        [UnityEngine.Header("Pooled Object")]
        [UnityEngine.SerializeField] int itemID;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] PoolData clusterData;
        [UnityEngine.SerializeField] PoolData poolData;

        UnityEngine.Coroutine lifetimeCR;

        public string ClusterName { get => clusterData.name; set => clusterData.name = value; }
        public int ClusterID { get => clusterData.id; set => clusterData.id = value; }

        public string PoolName { get => poolData.name; set => poolData.name = value; }
        public int PoolID { get => poolData.id; set => poolData.id = value; }

        public int ItemID { get => itemID; set => itemID = value; }

        public void Initialize(PoolData clusterData, PoolData poolData, int itemID, bool setActive = false)
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
            int milisenconds = UtilsMath.ConvertToMiliseconds(time);
            await Task.Delay(milisenconds);
            pool.Despawn(this);
        }
    }
}
