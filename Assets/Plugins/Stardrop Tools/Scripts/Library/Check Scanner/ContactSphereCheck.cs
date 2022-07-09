using System.Collections;
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Checks & broadcasts events based on ground touch
    /// </summary>
    public class ContactSphereCheck : ContactChecker
    {
        public float radius = .4f;

        public bool ContactCheck()
        {
            var contact = Physics.CheckSphere(target.position, radius, contactLayer);
            return ContactCheck(contact);
        }

#if UNITY_EDITOR
        [Header("Render")]
        [SerializeField] Color color = Color.red;
        [SerializeField] bool render;

        private void OnDrawGizmos()
        {
            if (render)
            {
                Gizmos.color = color;
                Gizmos.DrawWireSphere(target.position, radius);
            }
        }
#endif
    }
}