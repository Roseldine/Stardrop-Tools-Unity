
namespace StardropTools
{
    public abstract class ValueMeter : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] protected int meterID;

        protected bool isMin;
        protected bool isMax;

        protected UnityEngine.Coroutine lerpValueCR;
        protected bool isInitialized;

        public int MeterID { get => meterID; set => meterID = value; }

        public readonly BaseEvent OnReachMin = new BaseEvent();
        public readonly BaseEvent OnReachMax = new BaseEvent();
        public readonly BaseEvent OnValueChangeDry = new BaseEvent();

        public abstract void Initialize();
        public abstract void ResetValue();
    }
}