
using UnityEngine;

namespace StardropTools.FiniteStateMachine.EventFiniteStateMachine
{
    public class EventStateMachineComponent : BaseComponent, IStateMachine
    {
        [SerializeField] protected EventStateMachine stateMachine;
        public EventStateMachine EventStateMachine { get => stateMachine; set => stateMachine = value; }

        public EventState CurrentState { get => stateMachine.CurrentState; }
        public float TimeInCurrentState { get => stateMachine.CurrentState.TimeInState; }
        public int StateCount { get => stateMachine.StateCount; }
        public EventState GetState(int stateIndex) => stateMachine.GetState(stateIndex);

        public BaseEvent<int> OnStateEnter { get => stateMachine.OnStateEnter; }
        public BaseEvent<int> OnStateExit { get => stateMachine.OnStateEnter; }
        public BaseEvent<int> OnStateUpdate { get => stateMachine.OnStateEnter; }
        public BaseEvent<int> OnStatePause { get => stateMachine.OnStateEnter; }
        public BaseEvent<int> OnStateResume { get => stateMachine.OnStateEnter; }


        public override void Initialize()
        {
            base.Initialize();
            stateMachine.Initialize();
        }

        public void UpdateStateMachine()
            => stateMachine.UpdateStateMachine();

        public void ChangeState(int nextStateID)
            => stateMachine.ChangeState(nextStateID);

        public void InitializeStates()
            => stateMachine.InitializeStates();

        public void NextState()
            => stateMachine.NextState();

        public void PreviousState()
            => stateMachine.PreviousState();

        public void PauseState()
            => stateMachine.PauseState();

        public void ResumeState()
            => stateMachine.ResumeState();

        public void UpdateStateIDs()
            => stateMachine.UpdateStateIDs();

        public void AddState()
            => stateMachine.AddState();

        public void AddState(string stateName)
            => stateMachine.AddState(stateName);

        public void AddState(EventState state)
            => stateMachine.AddState(state);

        public void AddStates(EventState[] states)
            => stateMachine.AddStates(states);

        public void RemoveState()
            => stateMachine.RemoveState();

        public void RemoveState(int id)
            => stateMachine.RemoveState(id);

        public void RemoveState(EventState state)
            => stateMachine.RemoveState(state);

        public void ClearStates()
            => stateMachine.ClearStates();

        private void OnValidate()
        {
            if (StateCount > 0)
                stateMachine.UpdateStateIDs();
        }
    }
}