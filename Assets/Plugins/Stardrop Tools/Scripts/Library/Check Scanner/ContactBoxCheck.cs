using System.Collections;
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Checks & broadcasts events based on ground touch
    /// </summary>
    public class ContactBoxCheck : ContactChecker
    {
        public Vector3 scale = new Vector3(.4f, .05f, .4f);

        public bool ContactCheck()
        {
            var contact = Physics.CheckBox(target.position, scale, target.rotation, contactLayer);
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
                Utilities.DrawCube(target.position, scale, target.rotation);
            }
        }
#endif
    }
}