
namespace StardropTools.FiniteStateMachine
{
    public interface IState
    {
        public enum ExecutionPhaseEnum { None, Entering, Exited, Updating, Paused, Completed }

        public void EnterState();
        public void ExitState();
        public void UpdateState();
        public void PauseState();
        public void ResumeState();
        public void ChangeState(int stateID);
    }
}