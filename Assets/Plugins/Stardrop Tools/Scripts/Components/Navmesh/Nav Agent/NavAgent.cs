using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class NavAgent : TransformObject
    {
        [SerializeField] protected UnityEngine.AI.NavMeshAgent navAgent;

    }
}


