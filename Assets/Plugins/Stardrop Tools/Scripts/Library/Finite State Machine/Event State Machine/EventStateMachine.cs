﻿

namespace StardropTools.FiniteStateMachine.EventFiniteStateMachine
{
    [System.Serializable]
    public class EventStateMachine : IStateMachine
    {
        public int fsmID = 0;
        [UnityEngine.SerializeField] int startStateID = 0;
        [UnityEngine.SerializeField] EventState currentState;
        [UnityEngine.SerializeField] EventState previousState;
        [UnityEngine.SerializeField] System.Collections.Generic.List<EventState> states;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] bool log;

        public bool IsInitialized { get; private set; }
        public EventState CurrentState { get => currentState; }
        public float TimeInCurrentState { get => currentState.TimeInState; }
        public EventState GetState(int stateIndex) => states[stateIndex];

        public readonly CoreEvent<int> OnStateEnter = new CoreEvent<int>();
        public readonly CoreEvent<int> OnStateExit = new CoreEvent<int>();
        public readonly CoreEvent<int> OnStateUpdate = new CoreEvent<int>();
        public readonly CoreEvent<int> OnStatePause = new CoreEvent<int>();
        public readonly CoreEvent<int> OnStateResume = new CoreEvent<int>();

        public void Initialize()
        {
            if (IsInitialized)
                return;

            currentState = new EventState();
            InitializeStates();

            ChangeState(startStateID);
            IsInitialized = true;
        }

        public void UpdateStateMachine()
        {
            if (currentState != null)
            {
                currentState.HandleInput();
                currentState.UpdateState();

                OnStateUpdate?.Invoke(currentState.StateID);
            }
        }


        public virtual void ChangeState(int nextStateID)
        {
            if (currentState.StateID == nextStateID)
                return;

            var state = states[nextStateID];
            ChangeState(state);
        }

        public virtual void ChangeState(string nextStateName)
        {
            if (currentState.StateName == nextStateName)
                return;

            for (int i = 0; i < states.Count; i++)
            {
                if (states[i].StateName == nextStateName)
                {
                    ChangeState(i);
                    return;
                }
            }

            UnityEngine.Debug.Log("No state found with the name: " + nextStateName);
        }


        public void ChangeState(EventState nextState)
        {
            if (currentState == nextState)
                return;

            if (currentState != null)
            {
                currentState.ExitState();
                previousState = currentState;
                OnStateExit?.Invoke(currentState.StateID);
            }

            currentState = nextState;
            currentState.EnterState();
            OnStateEnter?.Invoke(currentState.StateID);

            if (log == true)
                UnityEngine.Debug.Log("<color=yellow> Changed to state: </color> <color=cyan>" + currentState.StateName + " (" + currentState.StateID + ")" + "</color>");
        }

        public void NextState() => ChangeState(currentState.NextStateID);

        public void PreviousState()
        {
            if (previousState != null)
                ChangeState(previousState);
        }

        public void PauseState()
        {
            currentState.PauseState();
            OnStatePause?.Invoke(currentState.StateID);
        }

        public void ResumeState()
        {
            currentState.ResumeState();
            OnStateResume?.Invoke(currentState.StateID);
        }

        public void InitializeStates()
        {
            if (states.Exists() == false)
                return;

            for (int i = 0; i < states.Count; i++)
                states[i].Initialize(this, i);
        }

        public void UpdateStateIDs()
        {
            if (states.Exists() == false)
                return;

            for (int i = 0; i < states.Count; i++)
                states[i].StateID = i;
        }

        public void AddState()
        {
            states.Add(new EventState());
            UpdateStateIDs();
        }

        public void AddState(string stateName)
        {
            states.Add(new EventState(stateName));
            UpdateStateIDs();
        }

        public void AddState(EventState state)
        {
            states.Add(state);
            UpdateStateIDs();
        }

        public void RemoveState()
        {
            return;
        }

        public void RemoveState(int id)
        {
            var state = GetState(id);
            states.RemoveSafe(state);
        }

        public void RemoveState(EventState state) => states.RemoveSafe(state);
    }
}