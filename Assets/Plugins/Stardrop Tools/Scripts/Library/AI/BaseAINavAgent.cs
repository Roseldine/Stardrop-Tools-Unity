
using UnityEngine;

namespace StardropTools.AI
{
    /// <summary>
    /// Base class for all AI Agents that use navmesh
    /// </summary>
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class BaseAINavAgent : BaseObject
    {
        public enum NavAgentState { idle, moving, paused, stopped }

        [Header("Nav Agent")]
        [SerializeField] protected NavAgentState agentState;
        [SerializeField] protected UnityEngine.AI.NavMeshAgent navAgent;
        [SerializeField] protected bool navAgentEnabled;
        
        public NavAgentState AgentState { get => agentState; }
        public bool IsAgentEnabled { get => navAgentEnabled; set => EnableAgent(value); }
        public bool IsStopped { get => navAgent.isStopped; }
        public bool IsOnNavmesh { get => navAgent.isOnNavMesh; }
        public bool HasPath { get => navAgent.hasPath; }
        public Vector3 Velocity { get => navAgent.velocity; set => SetVelocity(value); }


        public readonly BaseEvent OnEnableAgent = new BaseEvent();
        public readonly BaseEvent OnDisableAgent = new BaseEvent();

        public readonly BaseEvent OnSetDestination = new BaseEvent();
        public readonly BaseEvent OnStopAgent = new BaseEvent();
        public readonly BaseEvent OnResumeAgent = new BaseEvent();
        public readonly BaseEvent OnPauseAgent = new BaseEvent();

        protected void CheckIfValid()
        {
            if (IsAgentEnabled == false)
                Debug.Log("Agent NOT ENABLED!");

            if (IsOnNavmesh == false)
                Debug.Log("Agent NOT ON NAVMESH!");

            if (navAgent.hasPath == false)
                Debug.Log("Agent NO PATH!");
        }

        public void EnableAgent(bool value, bool ignoreChecks = false)
        {
            if (ignoreChecks == false && navAgentEnabled == value)
                return;

            navAgent.enabled = value;
            navAgent.isStopped = value;

            navAgentEnabled = value;
            CheckIfValid();

            if (value)
                OnEnableAgent?.Invoke();
            else
                OnDisableAgent?.Invoke();
        }

        public void SetSpeed(float speed)
            => navAgent.speed = speed;

        public void SetDestination(Vector3 destination)
        {
            navAgent.SetDestination(destination);
            CheckIfValid();

            agentState = NavAgentState.moving;
            OnSetDestination?.Invoke();
        }

        public void SetVelocity(Vector3 velocity)
        {
            navAgent.velocity = velocity;

            VelocityState();
            CheckIfValid();
        }


        public void StopAgent()
        {
            CheckIfValid();

            EnableAgent(false);
            navAgent.ResetPath();

            agentState = NavAgentState.stopped;
            OnStopAgent?.Invoke();
        }

        public void PauseAgent()
        {
            CheckIfValid();
            navAgent.isStopped = true;

            agentState = NavAgentState.paused;
            OnPauseAgent?.Invoke();
        }

        public void ResumeAgent()
        {
            CheckIfValid();

            navAgent.isStopped = false;

            if (navAgent.destination != null)
                SetDestination(navAgent.destination);

            if (navAgent.velocity.magnitude > 0)
                agentState = NavAgentState.moving;
            else
                agentState = NavAgentState.idle;

            VelocityState();
            OnResumeAgent?.Invoke();
        }

        void VelocityState()
        {
            if (navAgent.velocity.magnitude > 0)
                agentState = NavAgentState.moving;
            else
                agentState = NavAgentState.idle;
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (navAgent == null)
                navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }
    }
}


