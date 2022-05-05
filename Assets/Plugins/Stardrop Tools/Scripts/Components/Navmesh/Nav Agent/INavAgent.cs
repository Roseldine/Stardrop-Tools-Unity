using System.Collections;
using UnityEngine;

namespace StardropTools
{
    public interface INavAgent
    {
        public void ActivateAgent(bool value);
        public void EnableAgent();
        public void DisableAgent();
        public void SetDestination(Vector3 destination);
    }
}