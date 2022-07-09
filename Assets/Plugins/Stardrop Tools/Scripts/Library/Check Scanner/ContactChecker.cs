using System.Collections;
using UnityEngine;

namespace StardropTools
{
    public abstract class ContactChecker : MonoBehaviour
    {
        public LayerMask contactLayer;
        [SerializeField] protected bool hasContact;
        [Space]
        [SerializeField] protected Transform target;

        public bool HasContact { get => hasContact; }

        public readonly BaseEvent OnContactStart = new BaseEvent();
        public readonly BaseEvent OnContact = new BaseEvent();
        public readonly BaseEvent OnContactEnd = new BaseEvent();

        /// <summary>
        /// Checks & broadcasts events based on contact
        /// </summary>
        public bool ContactCheck(bool physicsContactBoolean)
        {
            if (hasContact != physicsContactBoolean)
            {
                // contact start
                if (physicsContactBoolean && hasContact == false)
                    OnContactStart?.Invoke();

                // contact end
                if (physicsContactBoolean == false && hasContact)
                    OnContactEnd?.Invoke();

                hasContact = physicsContactBoolean;
            }

            // is in contact
            else
                OnContact?.Invoke();

            return physicsContactBoolean;
        }
    }
}