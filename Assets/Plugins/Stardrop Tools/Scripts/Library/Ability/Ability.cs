
using UnityEngine;
using StardropTools.FiniteStateMachine.EventFiniteStateMachine;

namespace StardropTools.Ability
{
    public class Ability : EventStateMachineComponent
    {
        protected const float defaultDuration = .001f;
        public enum AbilityPhase { Ready, Casting, Executing, Recovery, Cooldown }

        [SerializeField] protected AbilityPhase phase;
        [SerializeField] protected new SingleLayerAnimation animation;
        [SerializeField] protected float duration;
        [SerializeField] protected bool preventOtherAbilities;
        [SerializeField] protected int animationID;
        [Space]
        [SerializeField] protected bool createStates;

        public AbilityPhase Phase { get => phase; }

        #region Events
        // Ready
        public BaseEvent OnReady { get => stateMachine.GetState(0).OnStateEnter; }
        public BaseEvent OnReadyEnd { get => stateMachine.GetState(0).OnStateExit; }

        // Casting
        public BaseEvent OnCastStart { get => stateMachine.GetState(1).OnStateEnter; }
        public BaseEvent OnCast { get => stateMachine.GetState(1).OnStateUpdate; }
        public BaseEvent OnCastEnd { get => stateMachine.GetState(1).OnStateExit; }

        // Executing
        public BaseEvent OnExecutionStart { get => stateMachine.GetState(2).OnStateEnter; }
        public BaseEvent OnExecution { get => stateMachine.GetState(2).OnStateUpdate; }
        public BaseEvent OnExecutionEnd { get => stateMachine.GetState(2).OnStateExit; }

        // Recovery
        public BaseEvent OnRecoveryStart { get => stateMachine.GetState(3).OnStateEnter; }
        public BaseEvent OnRecovery { get => stateMachine.GetState(3).OnStateUpdate; }
        public BaseEvent OnRecoveryEnd { get => stateMachine.GetState(3).OnStateExit; }

        // Cooldown
        public BaseEvent OnCooldownStart { get => stateMachine.GetState(4).OnStateEnter; }
        public BaseEvent OnCooldown { get => stateMachine.GetState(4).OnStateUpdate; }
        public BaseEvent OnCooldownEnd { get => stateMachine.GetState(4).OnStateExit; }

        #endregion // events

        public override void Initialize()
        {
            base.Initialize();

            OnStateEnter.AddListener(SetAbilityPhase);
        }

        // Ready state is infinite, so we must change to cast to start the loop
        public void StartAbility()
            => ChangeState(1);

        public virtual void UpdateAbility()
        {
            UpdateStateMachine();
        }

        public void SetAbilityPhase(int phaseID)
        {
            if (phaseID != (int)phase)
                phase = (AbilityPhase)phaseID;
        }

        public void PlayAnimation(int animID)
            => animation.CrossFadeAnimation(animID);

        public Vector3 ViewportRaycast(Camera camera, LayerMask mask)
        {
            Ray ray = camera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 1000, mask);

            return hit.point;
        }

        /// <summary>
        /// Creates a state for each Ability Phase enum element
        /// </summary>
        protected void CreateEssentialStates()
        {
            stateMachine.ClearStates();
            var tempStates = new System.Collections.Generic.List<EventState>();

            tempStates.Add(new EventState(0, "Ready", 1));
            tempStates.Add(new EventState(1, "Casting", 2));
            tempStates.Add(new EventState(2, "Executing", 3));
            tempStates.Add(new EventState(3, "Recovery", 4));
            tempStates.Add(new EventState(4, "Cooldown", 0));

            for (int i = 1; i < tempStates.Count; i++)
                tempStates[i].Duration = defaultDuration;

            stateMachine.AddStates(tempStates.ToArray());
        }

        private void OnValidate()
        {
            if (createStates)
            {
                CreateEssentialStates();
                createStates = false;
            }
        }
    }
}