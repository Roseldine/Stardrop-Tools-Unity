
using System.Threading.Tasks;


namespace StardropTools.Pool
{
    public class PooledObject : CoreTransform, IPoolable
    {
        [UnityEngine.Header("Pooled Object")]
        [UnityEngine.SerializeField] int itemID;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] string clusterName;
        [UnityEngine.SerializeField] int clusterID;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] string poolName;
        [UnityEngine.SerializeField] int poolID;

        UnityEngine.Coroutine lifetimeCR;

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

        public virtual void LifeTime(Pool pool, float time)
            => lifetimeCR = StartCoroutine(LifetimeCR(pool, time));

        System.Collections.IEnumerator LifetimeCR(Pool pool, float time)
        {
            yield return pool.GetWait(time);
            pool.Despawn(this);
        }

        public async void LifeTimeAsync(Pool pool, float time)
            => await LifetimeSync(pool, time);

        private async Task LifetimeSync(Pool pool, float time)
        {
            int milisenconds = MathUtility.ConvertToMiliseconds(time);
            await Task.Delay(milisenconds);
            pool.Despawn(this);
        }
    }
}