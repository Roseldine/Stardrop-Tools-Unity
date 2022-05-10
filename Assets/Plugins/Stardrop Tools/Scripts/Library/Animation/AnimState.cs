
namespace StardropTools
{
    [System.Serializable]
    public class AnimState
    {
        [UnityEngine.SerializeField] string stateName;
        [UnityEngine.SerializeField] int layer;
        [UnityEngine.SerializeField] float lengthInSeconds;
        [UnityEngine.Range(0, 1)] public float crossfade = .15f;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] AnimEvent[] events;

        public string StateName { get => stateName; }
        public int Layer { get => layer; }
        public float LengthInSeconds { get => lengthInSeconds; }
        public int EventCount { get => events.Exists() ? events.Length : 0; }
        public AnimEvent[] AnimEvents { get => events; }

        public AnimState()
        {
            crossfade = .15f;
        }

        public AnimState(string stateName, int layer, float crossfadeTime, float animLength)
        {
            this.stateName = stateName;
            this.layer = layer;
            crossfade = crossfadeTime;
            lengthInSeconds = animLength;
        }
    }
}