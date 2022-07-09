
using UnityEngine;
using StardropTools.FiniteStateMachine.EventFiniteStateMachine;

namespace StardropTools.Ability
{
    public class AbilityPhase : EventState
    {
        protected Ability ability;

        [SerializeField] protected float abilityDuration;
        [Space]
        [SerializeField] protected bool canMove;
        [SerializeField] protected bool canJump;
        [SerializeField] protected bool canBePrevented;
        [Space]
        [SerializeField] protected int animationID;

        public virtual void Initialize(Ability ability, int id)
        {
            this.ability = ability;
            stateMachine = ability.EventStateMachine;
            stateID = id;
        }

        public override void EnterState()
        {
            base.EnterState();
            ability.PlayAnimation(animationID);
        }
    }
}