

namespace StardropTools.FiniteStateMachine.EventFiniteStateMachine
{
    [System.Serializable]
    public class EventState
    {
        public enum ExecutionState { None, Activated, Completed, Running, Paused }

        [UnityEngine.SerializeField] protected string stateName;
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected int stateID;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] protected float timeInState;
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected float maxTime;
        [UnityEngine.Min(0)] [UnityEngine.SerializeField] protected int nextStateID = -1;

        EventStateMachine stateMachine;
        ExecutionState prevExecutionState;

        public string StateName { get => stateName; set => stateName = value; }
        public int StateID { get => stateID; set => stateID = value; }
        public float MaxTime { get => maxTime; set => maxTime = value; }
        public int NextStateID { get => nextStateID; set => nextStateID = value; }

        public float TimeInState { get => timeInState; }

        public bool IsInitialized { get; private set; }
        public ExecutionState ExecutionFase { get; protected set; }


        public readonly CoreEvent OnStateEnter = new CoreEvent();
        public readonly CoreEvent OnStateExit = new CoreEvent();
        public readonly CoreEvent OnStateInput = new CoreEvent();
        public readonly CoreEvent OnStateUpdate = new CoreEvent();
        public readonly CoreEvent OnStatePause = new CoreEvent();
        public readonly CoreEvent OnStateComplete = new CoreEvent();

        public EventState() { nextStateID = -1; }

        public EventState(int id)
        {
            stateID = id;
            nextStateID = -1;
        }

        public EventState(int id, string name)
        {
            stateID = id;
            stateName = name;
            nextStateID = -1;
        }

        public virtual void Initialize(EventStateMachine fsm)
        {
            if (IsInitialized)
                return;

            stateMachine = fsm;
            IsInitialized = true;
        }

        public virtual bool EnterState()
        {
            // check if already entered
            if (ExecutionFase == ExecutionState.Activated)
                return false;

            ChangeExecutionStage(ExecutionState.Activated);
            timeInState = 0;

            OnStateEnter?.Invoke();
            return true;
        }

        public virtual bool ExitState()
        {
            // check if already exited
            if (ExecutionFase == ExecutionState.Completed)
                return false;

            ChangeExecutionStage(ExecutionState.Completed);

            OnStateExit?.Invoke();
            return true;
        }

        public virtual void HandleInput()
        {
            // check if Paused or Completed
            if (ExecutionFase == ExecutionState.Paused || ExecutionFase == ExecutionState.Completed)
                return;

            OnStateInput?.Invoke();
        }

        public virtual void UpdateState()
        {
            // check if Paused or Completed
            if (ExecutionFase == ExecutionState.Paused || ExecutionFase == ExecutionState.Completed)
                return;

            // check if completion time reached
            if (maxTime > 0 && timeInState > maxTime)
            {
                if (nextStateID >= 0)
                    stateMachine.ChangeState(nextStateID);

                ChangeExecutionStage(ExecutionState.Completed);
            }

            timeInState += UnityEngine.Time.deltaTime;

            OnStateUpdate?.Invoke();
        }

        public void PauseState() => ChangeExecutionStage(ExecutionState.Paused);
        public void ResumeState() => ChangeExecutionStage(prevExecutionState);

        protected void ChangeExecutionStage(ExecutionState phase)
        {
            if (ExecutionFase == phase)
                return;

            ExecutionFase = phase;
            prevExecutionState = ExecutionFase;
        }
    }
}