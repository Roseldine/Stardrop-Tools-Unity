

namespace StardropTools.FiniteStateMachine.EventFiniteStateMachine
{
    public interface IEventStateMachined
    {
		BaseEvent SyncEventEnter(int stateIndex);
		BaseEvent SyncEventExit(int stateIndex);
		BaseEvent SyncEventUpdate(int stateIndex);

		EventState GetState(int index);
		void ChangeState(int stateIndex);
	}
}