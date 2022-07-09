

namespace StardropTools.FiniteStateMachine.FiniteStateMachineComponent
{
    public class FiniteStateMachine : BaseComponent, IStateMachine
    {
        public int fsmID = 0;
        [UnityEngine.SerializeField] protected int startStateID = 0;
        [UnityEngine.SerializeField] protected UnityEngine.Transform parentStates;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] protected AbstractState startState;
        [UnityEngine.SerializeField] protected AbstractState currentState;
        [UnityEngine.SerializeField] protected AbstractState previousState;
        [UnityEngine.SerializeField] protected System.Collections.Generic.List<AbstractState> states;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] bool getStates;
        [UnityEngine.SerializeField] bool log;

        public AbstractState CurrentState { get => currentState; }
        public float TimeInCurrentState { get => currentState.TimeInState; }
        public AbstractState GetState(int stateIndex) => states[stateIndex];

        public readonly BaseEvent<AbstractState> OnStateEnter = new BaseEvent<AbstractState>();
        public readonly BaseEvent<AbstractState> OnStateExit = new BaseEvent<AbstractState>();
        public readonly BaseEvent<AbstractState> OnStateUpdate = new BaseEvent<AbstractState>();
        public readonly BaseEvent<AbstractState> OnStatePause = new BaseEvent<AbstractState>();
        public readonly BaseEvent<AbstractState> OnStateResume = new BaseEvent<AbstractState>();

        public override void Initialize()
        {
            base.Initialize();

            GetStates();
            InitializeStates();
            ChangeState(startState);
        }

        public virtual void InitializeStates()
        {
            if (states.Count == 0)
            {
                UnityEngine.Debug.Log("State list is empty");
                return;
            }

            for (int i = 0; i < states.Count; i++)
                states[i].Initialize(this, i);
        }


        public void UpdateStateMachine()
        {
            if (currentState != null)
            {
                currentState.HandleInput();
                currentState.UpdateState();

                OnStateUpdate?.Invoke(currentState);
            }
        }


        public virtual void ChangeState(int nextStateID)
        {
            if (currentState != null && currentState.StateID == nextStateID)
                return;

            var state = states[nextStateID];
            ChangeState(state);
        }


        public void ChangeState(AbstractState nextState)
        {
            if (currentState == nextState)
                return;

            if (currentState != null)
            {
                currentState.ExitState();
                previousState = currentState;
                OnStateExit?.Invoke(currentState);
            }

            currentState = nextState;
            currentState.EnterState();
            OnStateEnter?.Invoke(currentState);

            if (log == true)
                UnityEngine.Debug.Log("<color=yellow> Changed to state: </color> <color=cyan>" + currentState.name + " (id: " + currentState.StateID + ")" + "</color>");
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
            OnStatePause?.Invoke(currentState);
        }

        public void ResumeState()
        {
            currentState.ResumeState();
            OnStateResume?.Invoke(currentState);
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

        public void AddState(AbstractState state)
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

        public void RemoveState(AbstractState state)
            => states.RemoveSafe(state);

        protected void GetStates()
        {
            if (states.Count != parentStates.childCount)
                states = Utilities.GetItems<AbstractState>(parentStates);

            startState = states[0];
        }
        
        protected virtual void OnValidate()
        {
            if (parentStates == null)
                parentStates = transform;

            if (getStates)
            {
                GetStates();
                getStates = false;
            }
        }
    }
}
