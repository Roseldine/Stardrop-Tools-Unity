
namespace StardropTools
{
    [System.Serializable]
    public class AnimState
    {
        [UnityEngine.SerializeField] string state;
        [UnityEngine.SerializeField] float length;
        [UnityEngine.Range(0, 1)] public float crossfade = .15f;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] AnimEvent[] events;

        public string State { get => state; }
        public float Length { get => length; }
        public int EventCount { get => events.Exists() ? events.Length : 0; }
        public AnimEvent[] AnimEvents { get => events; }

        public AnimState()
        {
            crossfade = .15f;
        }

        public AnimState(string stateName)
        {
            state = stateName;
            crossfade = .15f;
        }

        public AnimState(string stateName, float crossfadeTime)
        {
            state = stateName;
            crossfade = crossfadeTime;
        }

        public AnimState(string stateName, float crossfadeTime, float animLength)
        {
            state = stateName;
            crossfade = crossfadeTime;
            length = animLength;
        }
    }
}