
namespace StardropTools.Pool
{
    public interface IPoolable
    {
        public void OnSpawn();
        public void OnDespawn();
        public void LifeTime(PooledObjectPool pool, float time);
    }
}
