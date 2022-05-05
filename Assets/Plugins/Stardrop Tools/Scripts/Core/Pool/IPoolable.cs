
namespace StardropTools.Pool
{
    public interface IPoolable
    {
        public void OnSpawn();
        public void OnDespawn();
        public void LifeTime(Pool pool, float time);
    }
}
