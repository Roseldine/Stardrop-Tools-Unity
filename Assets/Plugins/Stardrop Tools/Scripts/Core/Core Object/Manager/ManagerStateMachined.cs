
using UnityEngine;
using StardropTools.EventFSM;

namespace StardropTools
{
    /// <summary>
    /// Variant of the CoreManager, that has an Event State Machine
    /// </summary>
    [RequireComponent(typeof(EventStateMachine))]
    public abstract class ManagerStateMachined : Manager, IStateMachined
    {
        [SerializeField] protected EventStateMachine eStateMachine;
        public EventState CurrentState { get => eStateMachine.CurrentState; }
        public virtual EventState GetState(int index) => eStateMachine.GetState(index);


        public override void Initialize()
        {
            base.Initialize();
            coreData.SetUpdate(true);
            eStateMachine.Initialize();
        }

        public override void UpdateResource()
        {
            base.UpdateResource();
            eStateMachine.UpdateStateMachine();
        }


        public CoreEvent SyncEventEnter(int stateIndex)
        => GetState(stateIndex).OnStateEnter;
        public CoreEvent SyncEventExit(int stateIndex)
            => GetState(stateIndex).OnStateExit;
        public CoreEvent SyncEventUpdate(int stateIndex)
            => GetState(stateIndex).OnStateUpdate;

        public virtual void ChangeState(int stateIndex) => eStateMachine.ChangeState(stateIndex);
    }
}