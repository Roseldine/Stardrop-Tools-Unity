
using UnityEngine;

namespace StardropTools.AI
{
    public class AINavAgentStateMachine : StardropTools.FiniteStateMachine.FiniteStateMachineComponent.FiniteStateMachine
    {
        [SerializeField] protected AINavAgent agent;

        public AINavAgent Agent { get => agent; }
    }
}