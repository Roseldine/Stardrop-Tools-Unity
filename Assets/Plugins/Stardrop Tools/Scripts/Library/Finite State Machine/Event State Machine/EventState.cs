

namespace StardropTools.FiniteStateMachine.EventFiniteStateMachine
{
    [System.Serializable]
    public class EventState : IState
    {
        [UnityEngine.SerializeField]                      protected string stateName;
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected int stateID;
        [UnityEngine.Space]
        [UnityEngine.SerializeField]                      protected float timeInState;
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected float duration;
        [UnityEngine.Space]
        [UnityEngine.Min(-1)] [UnityEngine.SerializeField] protected int nextStateID = -1;

        protected EventStateMachine stateMachine;
        protected IState.ExecutionPhaseEnum prevExecutionState;

        public string StateName { get => stateName; set => stateName = value; }
        public int StateID { get => stateID; set => stateID = value; }
        public float Duration { get => duration; set => duration = value; }
        public int NextStateID { get => nextStateID; set => nextStateID = value; }

        public float TimeInState { get => timeInState; }

        public bool IsInitialized { get; private set; }
        public IState.ExecutionPhaseEnum ExecutionPhase { get; protected set; }


        public readonly BaseEvent OnStateInitialize = new BaseEvent();
        public readonly BaseEvent OnStateEnter = new BaseEvent();
        public readonly BaseEvent OnStateExit = new BaseEvent();
        public readonly BaseEvent OnStateInput = new BaseEvent();
        public readonly BaseEvent OnStateUpdate = new BaseEvent();
        public readonly BaseEvent OnStatePause = new BaseEvent();
        public readonly BaseEvent OnStateComplete = new BaseEvent();


        #region Constructor
        public EventState() { nextStateID = -1; }

        public EventState(int id)
        {
            stateID = id;
            nextStateID = -1;
        }

        public EventState(string name)
        {
            stateName = name;
            nextStateID = -1;
        }

        public EventState(int id, string name)
        {
            stateID = id;
            stateName = name;
            nextStateID = -1;
        }

        public EventState(int id, string name, int nextStateID)
        {
            stateID = id;
            stateName = name;
            this.nextStateID = nextStateID;
        }
        #endregion // constructor

        public virtual void Initialize(EventStateMachine fsm, int id)
        {
            if (IsInitialized)
                return;

            stateID = id;
            stateMachine = fsm;
            IsInitialized = true;

            OnStateInitialize?.Invoke();
        }

        public virtual void EnterState()
        {
            // return if already entered
            if (ExecutionPhase == IState.ExecutionPhaseEnum.Entering)
                return;

            ChangeExecutionStage(IState.ExecutionPhaseEnum.Entering);
            timeInState = 0;
        }

        public virtual void ExitState()
        {
            // return if already exited
            if (ExecutionPhase == IState.ExecutionPhaseEnum.Exited)
                return;

            ChangeExecutionStage(IState.ExecutionPhaseEnum.Exited);
        }

        public virtual void HandleInput()
        {
            // return if Paused or Completed
            if (ExecutionPhase == IState.ExecutionPhaseEnum.Paused || ExecutionPhase == IState.ExecutionPhaseEnum.Completed || ExecutionPhase == IState.ExecutionPhaseEnum.Exited)
                return;

            OnStateInput?.Invoke();
        }

        public virtual void UpdateState()
        {
            // return if Paused or Completed
            if (ExecutionPhase == IState.ExecutionPhaseEnum.Paused || ExecutionPhase == IState.ExecutionPhaseEnum.Completed || ExecutionPhase == IState.ExecutionPhaseEnum.Exited)
                return;

            // check if completion time reached
            if (duration > 0 && timeInState > duration)
            {
                if (nextStateID >= 0)
                    stateMachine.ChangeState(nextStateID);

                ChangeExecutionStage(IState.ExecutionPhaseEnum.Completed);
            }

            timeInState += UnityEngine.Time.deltaTime;

            OnStateUpdate?.Invoke();
        }

        public void PauseState()
        {
            // do nothing if state isn't updating
            if (ExecutionPhase != IState.ExecutionPhaseEnum.Updating)
                return;

            ChangeExecutionStage(IState.ExecutionPhaseEnum.Paused);
        }

        public void ResumeState()
        {
            // do nothing if state isn't paused
            if (ExecutionPhase != IState.ExecutionPhaseEnum.Paused)
                return;

            ChangeExecutionStage(prevExecutionState);
        }

        public void ChangeState(int stateID) => stateMachine.ChangeState(stateID);

        protected void ChangeExecutionStage(IState.ExecutionPhaseEnum phase)
        {
            if (ExecutionPhase == phase)
                return;

            ExecutionPhase = phase;
            prevExecutionState = ExecutionPhase;

            switch (phase)
            {
                case IState.ExecutionPhaseEnum.None:
                    break;

                case IState.ExecutionPhaseEnum.Entering:
                    OnStateEnter?.Invoke();
                    break;

                case IState.ExecutionPhaseEnum.Exited:
                    OnStateExit?.Invoke();
                    break;

                case IState.ExecutionPhaseEnum.Updating:
                    OnStateUpdate?.Invoke();
                    break;

                case IState.ExecutionPhaseEnum.Paused:
                    OnStatePause?.Invoke();
                    break;

                case IState.ExecutionPhaseEnum.Completed:
                    OnStateComplete?.Invoke();
                    break;
            }
        }
    }
}