
using UnityEngine;

namespace StardropTools.AI
{
    /// <summary>
    /// Base class for all AI Agents that use navmesh & state machines
    /// </summary>
    [RequireComponent(typeof(Pool.PooledObject))]
    public abstract class AINavAgent : BaseAINavAgent
    {
        [SerializeField] new protected SingleLayerAnimation animation;
        [SerializeField] protected Damageable damageable;
        [SerializeField] protected FiniteStateMachine.FiniteStateMachineComponent.FiniteStateMachine stateMachine;
        [SerializeField] protected Transform parentGraphic;

        protected Pool.PooledObject pooled;

        public SingleLayerAnimation Animation { get => animation; }
        public Damageable Damageable { get => damageable; }
        public Transform ParentGraphic { get => parentGraphic; }


        public override void Initialize()
        {
            base.Initialize();

            stateMachine.Initialize();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (pooled == null)
                pooled = GetComponent<Pool.PooledObject>();
        }

        public virtual void ChangeState(int stateID)
            => stateMachine.ChangeState(stateID);

        public virtual void DespawnPooled()
            => pooled.Despawn();
    }
}