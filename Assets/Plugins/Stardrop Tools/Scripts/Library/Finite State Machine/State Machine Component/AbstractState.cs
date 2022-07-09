

namespace StardropTools.FiniteStateMachine.FiniteStateMachineComponent
{
    #region Copy Body
    /*
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
    */
    #endregion // copy body

    public abstract class AbstractState : UnityEngine.MonoBehaviour, IState
    {
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected int stateID;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] protected float timeInState;
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected float maxTime;
        [UnityEngine.Space]
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected int nextStateID = -1;

        protected FiniteStateMachine stateMachine;
        protected IState.ExecutionPhaseEnum prevExecutionState;

        public int StateID { get => stateID; set => stateID = value; }
        public float MaxTime { get => maxTime; set => maxTime = value; }
        public int NextStateID { get => nextStateID; set => nextStateID = value; }

        public float TimeInState { get => timeInState; }

        public bool IsInitialized { get; private set; }
        public IState.ExecutionPhaseEnum ExecutionPhase { get; protected set; }

        #region Events

        public readonly BaseEvent OnStateInitialize = new BaseEvent();
        public readonly BaseEvent OnStateEnter = new BaseEvent();
        public readonly BaseEvent OnStateExit = new BaseEvent();
        public readonly BaseEvent OnStateInput = new BaseEvent();
        public readonly BaseEvent OnStateUpdate = new BaseEvent();
        public readonly BaseEvent OnStatePause = new BaseEvent();
        public readonly BaseEvent OnStateResume = new BaseEvent();
        public readonly BaseEvent OnStateComplete = new BaseEvent();

        public readonly BaseEvent<AbstractState> OnStateInitializeID = new BaseEvent<AbstractState>();
        public readonly BaseEvent<AbstractState> OnStateEnterID = new BaseEvent<AbstractState>();
        public readonly BaseEvent<AbstractState> OnStateExitID = new BaseEvent<AbstractState>();
        public readonly BaseEvent<AbstractState> OnStateInputID = new BaseEvent<AbstractState>();
        public readonly BaseEvent<AbstractState> OnStateUpdateID = new BaseEvent<AbstractState>();
        public readonly BaseEvent<AbstractState> OnStatePauseID = new BaseEvent<AbstractState>();
        public readonly BaseEvent<AbstractState> OnStateResumeID = new BaseEvent<AbstractState>();
        public readonly BaseEvent<AbstractState> OnStateCompleteID = new BaseEvent<AbstractState>();

        #endregion // events

        public virtual void Initialize(FiniteStateMachine fsm, int id)
        {
            if (IsInitialized)
                return;

            stateID = id;
            stateMachine = fsm;
            IsInitialized = true;

            OnStateInitialize?.Invoke();
            OnStateInitializeID?.Invoke(this);
        }

        public virtual void EnterState()
        {
            // return if already entered
            if (ExecutionPhase == IState.ExecutionPhaseEnum.Entering)
                return;

            ChangeExecutionStage(IState.ExecutionPhaseEnum.Entering);
            timeInState = 0;

            OnStateEnter?.Invoke();
            OnStateEnterID?.Invoke(this);
        }

        public virtual void ExitState()
        {
            // return if already exited
            if (ExecutionPhase == IState.ExecutionPhaseEnum.Exited)
                return;

            ChangeExecutionStage(IState.ExecutionPhaseEnum.Exited);

            OnStateExit?.Invoke();
            OnStateExitID?.Invoke(this);
        }

        /// <summary>
        /// Method for Handling Input decisions
        /// </summary>
        public virtual void HandleInput()
        {
            // return if Paused or Completed
            if (ExecutionPhase == IState.ExecutionPhaseEnum.Paused || ExecutionPhase == IState.ExecutionPhaseEnum.Completed || ExecutionPhase == IState.ExecutionPhaseEnum.Exited)
                return;

            OnStateInput?.Invoke();
            OnStateInputID?.Invoke(this);
        }


        /// <summary>
        /// Method for Updating state behaviour
        /// </summary>
        public virtual void UpdateState()
        {
            // return if Paused or Completed
            if (ExecutionPhase == IState.ExecutionPhaseEnum.Paused || ExecutionPhase == IState.ExecutionPhaseEnum.Completed || ExecutionPhase == IState.ExecutionPhaseEnum.Exited)
                return;

            // check if completion time reached
            if (maxTime > 0 && timeInState > maxTime)
            {
                if (nextStateID >= 0)
                    stateMachine.ChangeState(nextStateID);

                ChangeExecutionStage(IState.ExecutionPhaseEnum.Completed);
            }

            timeInState += UnityEngine.Time.deltaTime;

            OnStateUpdate?.Invoke();
            OnStateUpdateID?.Invoke(this);
        }

        public void PauseState()
        {
            // do nothing if state isn't updating
            if (ExecutionPhase != IState.ExecutionPhaseEnum.Updating)
                return;

            ChangeExecutionStage(IState.ExecutionPhaseEnum.Paused);

            OnStatePause?.Invoke();
            OnStatePauseID?.Invoke(this);
        }

        public void ResumeState()
        {
            // do nothing if state isn't paused
            if (ExecutionPhase != IState.ExecutionPhaseEnum.Paused)
                return;

            ChangeExecutionStage(prevExecutionState);

            OnStateResume?.Invoke();
            OnStateResumeID?.Invoke(this);
        }

        public virtual void ChangeState(int stateID) => stateMachine.ChangeState(stateID);

        protected void ChangeExecutionStage(IState.ExecutionPhaseEnum phase)
        {
            if (ExecutionPhase == phase)
                return;

            ExecutionPhase = phase;
            prevExecutionState = ExecutionPhase;

            /*
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
            */
        }
    }
}
