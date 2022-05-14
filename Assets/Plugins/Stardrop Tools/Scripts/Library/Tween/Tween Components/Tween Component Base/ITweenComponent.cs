
namespace StardropTools.Tween
{
    public interface ITweenComponent
    {
        public enum GlobalOrLocal { global, local }

        public void InitializeTween();
        public void PauseTween();
        public void StopTween();
    }
}