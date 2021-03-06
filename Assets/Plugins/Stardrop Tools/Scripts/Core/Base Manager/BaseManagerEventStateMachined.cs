
using UnityEngine;
using StardropTools.FiniteStateMachine;
using StardropTools.FiniteStateMachine.EventFiniteStateMachine;

namespace StardropTools
{
    [RequireComponent(typeof(EventStateMachineComponent))]
    public class BaseManagerEventStateMachined : BaseManager, IStateMachine
    {
        [SerializeField] protected EventStateMachineComponent eventStateMachine;
        [SerializeField] protected bool createStates;

        public EventState CurrentState { get => eventStateMachine.CurrentState; }
        public float TimeInCurrentState { get => eventStateMachine.CurrentState.TimeInState; }
        public EventState GetState(int stateIndex) => eventStateMachine.GetState(stateIndex);

        public BaseEvent<int> OnStateEnter { get => eventStateMachine.OnStateEnter; }
        public BaseEvent<int> OnStateExit { get => eventStateMachine.OnStateEnter; }
        public BaseEvent<int> OnStateUpdate { get => eventStateMachine.OnStateEnter; }
        public BaseEvent<int> OnStatePause { get => eventStateMachine.OnStateEnter; }
        public BaseEvent<int> OnStateResume { get => eventStateMachine.OnStateEnter; }


        public override void Initialize()
        {
            base.Initialize();

            eventStateMachine.Initialize();
        }

        protected virtual void CreateEventStates()
        {

        }


        protected BaseEvent SyncEnter(int stateID)
        => GetState(stateID).OnStateEnter;

        protected BaseEvent SyncUpdate(int stateID)
            => GetState(stateID).OnStateUpdate;

        protected BaseEvent SyncExit(int stateID)
            => GetState(stateID).OnStateExit;


        public void UpdateStateMachine()
            => eventStateMachine.UpdateStateMachine();

        public virtual void ChangeState(int nextStateID)
            => eventStateMachine.ChangeState(nextStateID);

        public void InitializeStates()
            => eventStateMachine.InitializeStates();

        public void NextState()
            => eventStateMachine.NextState();

        public void PreviousState()
            => eventStateMachine.PreviousState();

        public void PauseState()
            => eventStateMachine.PauseState();

        public void ResumeState()
            => eventStateMachine.ResumeState();

        public void UpdateStateIDs()
            => eventStateMachine.UpdateStateIDs();

        public void AddState()
            => eventStateMachine.AddState();

        public void AddState(string stateName)
            => eventStateMachine.AddState(stateName);

        public void AddState(EventState state)
            => eventStateMachine.AddState(state);

        public void RemoveState()
            => eventStateMachine.RemoveState();

        public void RemoveState(int id)
            => eventStateMachine.RemoveState(id);

        public void RemoveState(EventState state)
            => eventStateMachine.RemoveState(state);

        protected override void OnValidate()
        {
            base.OnValidate();

            if (eventStateMachine == null)
                eventStateMachine = GetComponent<EventStateMachineComponent>();

            if (createStates)
            {
                CreateEventStates();
                createStates = false;
            }
        }
    }
}