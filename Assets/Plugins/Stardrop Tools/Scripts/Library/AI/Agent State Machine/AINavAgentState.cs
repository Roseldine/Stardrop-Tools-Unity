
using StardropTools.FiniteStateMachine.FiniteStateMachineComponent;

namespace StardropTools.AI
{
    public abstract class AINavAgentState : AbstractState
    {
        protected AINavAgentStateMachine agentStateMachine { get => stateMachine as AINavAgentStateMachine; }
        protected AINavAgent agent { get => agentStateMachine.Agent; }
        protected AINavAgent.NavAgentState navAgentState { get => agent.AgentState; }
        protected new SingleLayerAnimation animation { get => agent.Animation; }
        protected UnityEngine.AI.NavMeshAgent navAgent;

        public override void Initialize(FiniteStateMachine.FiniteStateMachineComponent.FiniteStateMachine fsm, int id)
        {
            base.Initialize(fsm, id);

            navAgent = agent.GetComponent<UnityEngine.AI.NavMeshAgent>();
        }


        public override void EnterState()
        {
            base.EnterState();
        }


        public override void ExitState()
        {
            base.ExitState();
        }


        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }
    }
}