

namespace StardropTools.FiniteStateMachine.EventFiniteStateMachine
{
    [System.Serializable]
    public class EventStateMachine : IStateMachine
    {
        public int fsmID = 0;
        [UnityEngine.SerializeField] int startStateID = 0;
        [UnityEngine.SerializeField] EventState currentState;
        [UnityEngine.SerializeField] EventState previousState;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] System.Collections.Generic.List<EventState> states;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] bool log;

        public bool IsInitialized { get; private set; }
        public EventState CurrentState { get => currentState; }
        public float TimeInCurrentState { get => currentState.TimeInState; }
        public int StateCount { get { if (states.Exists()) return states.Count; else return 0; } }
        public EventState GetState(int stateIndex) => states[stateIndex];

        public readonly BaseEvent<int> OnStateEnter = new BaseEvent<int>();
        public readonly BaseEvent<int> OnStateExit = new BaseEvent<int>();
        public readonly BaseEvent<int> OnStateUpdate = new BaseEvent<int>();
        public readonly BaseEvent<int> OnStatePause = new BaseEvent<int>();
        public readonly BaseEvent<int> OnStateResume = new BaseEvent<int>();


        public EventStateMachine() { }

        public EventStateMachine(int id)
        {
            fsmID = id;
        }
        public EventStateMachine(EventState[] states)
        {
            this.states.AddArrayToList(states);
        }

        public EventStateMachine(int id, EventState[] states)
        {
            fsmID = id;
            this.states.AddArrayToList(states);
        }



        public void Initialize()
        {
            if (IsInitialized)
                return;

            InitializeStates();
            currentState = new EventState(-1);
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

            ChangeState(states[nextStateID]);
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
            if (currentState.StateID == nextState.StateID)
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
            return;
        }

        public void AddState(bool updateIDs = true)
        {
            states.Add(new EventState());

            if (updateIDs)
                UpdateStateIDs();
        }

        public void AddState(string stateName, bool updateIDs = true)
        {
            states.Add(new EventState(stateName));

            if (updateIDs)
                UpdateStateIDs();
        }

        public void AddState(EventState state, bool updateIDs = true)
        {
            states.Add(state);

            if (updateIDs)
                UpdateStateIDs();
        }

        public void AddStates(EventState[] states)
        {
            foreach (var state in states)
                this.states.Add(state);

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

            UpdateStateIDs();
        }

        public void RemoveState(EventState state)
            => states.RemoveSafe(state);

        public void RemoveStates(EventState[] statesToRemove)
        {
            foreach (var state in statesToRemove)
                states.RemoveSafe(state);

            UpdateStateIDs();
        }

        public void ClearStates()
            => states.Clear();
    }
}