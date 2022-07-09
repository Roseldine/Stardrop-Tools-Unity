
using UnityEngine;

namespace StardropTools
{
    public class AnimationEventDetector : MonoBehaviour
    {
        [SerializeField] bool debug;

        public readonly BaseEvent<int> OnAnimEvent = new BaseEvent<int>();

        public void AnimationEvent(int eventID)
        {
            if (debug)
                Debug.LogFormat("Anim event: {0}, detected!", eventID);

            OnAnimEvent?.Invoke(eventID);
        }
    }
}
