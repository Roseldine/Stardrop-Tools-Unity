
using System.Threading.Tasks;


namespace StardropTools.Pool
{
    public class PooledObject : BaseObject, IPoolable
    {
        [UnityEngine.Header("Pooled Object")]
        [UnityEngine.SerializeField] protected PooledObjectPool pool;
        [UnityEngine.SerializeField] protected int itemID;

        protected UnityEngine.Coroutine lifetimeCR;


        public string ClusterName { get => pool.clusterData.name; set => pool.clusterData.name = value; }
        public int ClusterID { get => pool.clusterData.id; set => pool.clusterData.id = value; }

        public string PoolName { get => pool.poolData.name; set => pool.poolData.name = value; }
        public int PoolID { get => pool.poolData.id; set => pool.poolData.id = value; }
        public int PoolHash { get => pool.GetHashCode(); }

        public int ItemID { get => itemID; set => itemID = value; }


        public readonly BaseEvent OnSpawned = new BaseEvent();
        public readonly BaseEvent OnDespawned = new BaseEvent();


        public void Initialize(PooledObjectPool pool, int itemID, bool setActive = false)
        {
            this.pool = pool;
            this.itemID = itemID;

            SetActive(setActive);
            Initialize();
        }

        public virtual void OnSpawn()
            => OnSpawned?.Invoke();

        public virtual void OnDespawn()
        {
            if (lifetimeCR != null)
                StopCoroutine(lifetimeCR);

            OnDespawned?.Invoke();
        }

        public virtual void Despawn()
        {
            OnDespawn();
            pool.Despawn(this);
        }

        public virtual void LifeTime(PooledObjectPool pool, float time)
            => lifetimeCR = StartCoroutine(LifetimeCR(pool, time));

        System.Collections.IEnumerator LifetimeCR(PooledObjectPool pool, float time)
        {
            yield return pool.GetWait(time);
            pool.Despawn(this);
        }

        public async void LifeTimeAsync(PooledObjectPool pool, float time)
            => await LifetimeSync(pool, time);

        private async Task LifetimeSync(PooledObjectPool pool, float time)
        {
            int milisenconds = UtilsMath.ConvertToMiliseconds(time);
            await Task.Delay(milisenconds);
            pool.Despawn(this);
        }
    }
}
