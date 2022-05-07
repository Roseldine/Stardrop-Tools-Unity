

namespace StardropTools.FiniteStateMachine.EventFiniteStateMachine
{
    public class EventStateMachine : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] int startStateID = 0;
        [UnityEngine.SerializeField] EventState currentState;
        [UnityEngine.SerializeField] EventState[] states;
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
            SetStateIDs();

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

        public void ChangeState(EventState nextState)
        {
            if (currentState == nextState)
                return;

            if (currentState != null)
            {
                currentState.ExitState();
                OnStateExit?.Invoke(currentState.StateID);
            }

            currentState = nextState;
            currentState.EnterState();
            OnStateEnter?.Invoke(currentState.StateID);

            if (log == true)
                UnityEngine.Debug.Log("<color=yellow> Changed to state: </color> <color=cyan>" + currentState.StateName + " (" + currentState.StateID +")" + "</color>");
        }

        public virtual void ChangeState(int nextStateID)
        {
            var state = states[nextStateID];
            ChangeState(state);
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

        protected void SetStateIDs()
        {
            if (states.Exists() == false)
                return;

            int count = 0;
            foreach (EventState state in states)
            {
                state.Initialize(this);
                state.StateID = count;
                count++;
            }
        }

        protected virtual void OnValidate()
        {
            SetStateIDs();
        }
    }
}