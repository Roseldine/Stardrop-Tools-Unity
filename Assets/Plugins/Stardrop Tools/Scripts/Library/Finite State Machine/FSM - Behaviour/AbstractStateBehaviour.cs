

namespace StardropTools.FiniteStateMachine.StateMachineBehaviour
{
    #region Copy Body
    /*
    public override bool EnterState()
    {
        base.EnterState();
    
        return true;
    }
    
    
    public override bool ExitState()
    {
        base.ExitState();
    
        return true;
    }
    
    
    public override void HandleInput()
    {
        base.HandleInput();
    }
    
    public override void UpdateState()
    {
        base.UpdateState();
    }
    
    
    public override void PauseState()
    {
        base.PauseState();
    }
    */
    #endregion // copy body

    //[CreateAssetMenu(fileName = "New State", menuName = "Finite State Machine / Create New State")]
    public abstract class AbstractStateBehaviour : UnityEngine.MonoBehaviour
    {
        public enum ExecutionState { Entering, Exited, Updating, Paused, Completed }
    
        [UnityEngine.SerializeField] protected FiniteStateMachineBehaviour stateMachine;
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected int stateId;
        [UnityEngine.SerializeField] protected float timeInState;
        [UnityEngine.Space]
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected int nextStateID;
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected float maxTime;
    
        ExecutionState prevExecutionState;
        
        public bool IsInitialized { get; protected set; }
        public ExecutionState ExecutionFase { get; protected set; }
        public int StateID { get => stateId; }
        public float TimeInState { get => timeInState; }
    
    
        public readonly CoreEvent OnStateEnter = new CoreEvent();
        public readonly CoreEvent OnStateExit = new CoreEvent();

        public readonly CoreEvent OnStateHandleInput = new CoreEvent();
        public readonly CoreEvent OnStateUpdate = new CoreEvent();
        
        public readonly CoreEvent OnStatePause = new CoreEvent();
        public readonly CoreEvent OnStateResume = new CoreEvent();
        
        public readonly CoreEvent OnStateComplete = new CoreEvent();
    
    
        public virtual void Initialize(FiniteStateMachineBehaviour fsm, int id)
        {
            if (IsInitialized)
                return;

            stateId = id;
            stateMachine = fsm;
            IsInitialized = true;
        }
    
    
        public virtual bool EnterState()
        {
            // check if already entered
            if (ExecutionFase == ExecutionState.Entering)
                return false;
    
            ChangeExecutionStage(ExecutionState.Entering);
            timeInState = 0;
    
            return true;
        }
    
    
        public virtual bool ExitState()
        {
            // check if already exited
            if (ExecutionFase == ExecutionState.Exited)
                return false;
    
            ChangeExecutionStage(ExecutionState.Exited);
    
            return true;
        }
    
    
        public virtual void HandleInput()
        {
            // check if Paused or Completed
            //if (ExecutionFase == ExecutionState.Paused || ExecutionFase == ExecutionState.Completed || ExecutionFase == ExecutionState.Exited)
            if (ExecutionFase == ExecutionState.Paused || ExecutionFase == ExecutionState.Exited)
                return;

            OnStateHandleInput?.Invoke();
        }
    
        public virtual void UpdateState()
        {
            // check if Paused or Completed
            if (ExecutionFase == ExecutionState.Paused || ExecutionFase == ExecutionState.Completed || ExecutionFase == ExecutionState.Exited)
                return;

            if (ExecutionFase != ExecutionState.Updating)
                ChangeExecutionStage(ExecutionState.Updating);
    
            // check if completion time reached
            if (maxTime > 0 && timeInState >= maxTime && ExecutionFase != ExecutionState.Completed)
            {
                ChangeExecutionStage(ExecutionState.Completed);

                if (nextStateID >= 0)
                    stateMachine.ChangeState(nextStateID);
            }
    
            timeInState += UnityEngine.Time.deltaTime;
    
            OnStateUpdate?.Invoke();
        }
    
    
        public virtual void PauseState()
            => ChangeExecutionStage(ExecutionState.Paused);

        public void ResumeState()
        {
            if (ExecutionFase != ExecutionState.Paused)
                return;

            ChangeExecutionStage(ExecutionState.Updating);
        }
    
        protected void ChangeExecutionStage(ExecutionState phase)
        {
            if (ExecutionFase == phase)
                return;
    
            ExecutionFase = phase;
            prevExecutionState = ExecutionFase;

            if (phase == ExecutionState.Entering)
                OnStateEnter?.Invoke();

            else if (phase == ExecutionState.Exited)
                OnStateExit?.Invoke();

            else if (phase == ExecutionState.Paused)
                OnStatePause?.Invoke();

            else if (phase == ExecutionState.Updating && prevExecutionState == ExecutionState.Paused)
                OnStateResume?.Invoke();

            else if (phase == ExecutionState.Completed)
                OnStateComplete?.Invoke();
        }

        protected virtual void ChangeState(int nextStateID)
        {
            if (stateMachine != null)
                stateMachine.ChangeState(nextStateID);
        }
    }
}
