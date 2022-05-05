
using UnityEngine;
using StardropTools.EventFSM;

namespace StardropTools
{
	[RequireComponent(typeof(EventStateMachine))]
	public class TransformObjectStateMachined : TransformObject, IStateMachined
	{
		[SerializeField] protected EventStateMachine eStateMachine;
		public EventState CurrentState { get => eStateMachine.CurrentState; }
		public virtual EventState GetState(int index) => eStateMachine.GetState(index);

		public CoreEvent SyncEventEnter(int stateIndex)
		=> GetState(stateIndex).OnStateEnter;
		public CoreEvent SyncEventExit(int stateIndex)
			=> GetState(stateIndex).OnStateExit;
		public CoreEvent SyncEventUpdate(int stateIndex)
			=> GetState(stateIndex).OnStateUpdate;

		public virtual void ChangeState(int stateIndex) => eStateMachine.ChangeState(stateIndex);

		protected override void OnValidate()
		{
			base.OnValidate();

			if (eStateMachine == null)
				eStateMachine = GetComponent<EventStateMachine>();
		}
	}
}