
using UnityEngine;

namespace StardropTools.FSM.StateMachineBehaviour
{
    public class FiniteStateMachineBehaviour : MonoBehaviour
    {
        [Header("State Viewer")]
        [SerializeField] protected AbstractStateBehaviour startingState;
        [SerializeField] protected AbstractStateBehaviour currentState;
        [SerializeField] protected AbstractStateBehaviour previousState;

        [Header("States")]
        [SerializeField] protected Transform parentStates;
        [SerializeField] protected AbstractStateBehaviour[] states;
#if UNITY_EDITOR
        [SerializeField] protected bool getStates;
        [Space]
        [SerializeField] protected int stateIndex;
        [SerializeField] protected bool changeState;
#endif

        [Header("Debug")]
        public bool log;

        public bool IsInitialized { get; private set; }

        public readonly CoreEvent<int> OnStateEnter = new CoreEvent<int>();
        public readonly CoreEvent<int> OnStateExit = new CoreEvent<int>();
        public readonly CoreEvent<int> OnStateUpdate = new CoreEvent<int>();
        public readonly CoreEvent<int> OnStatePause = new CoreEvent<int>();
        public readonly CoreEvent<int> OnStateResume = new CoreEvent<int>();

        public virtual void Initialize()
        {
            if (IsInitialized)
                return;

            GetStates();
            ChangeState(startingState);

            IsInitialized = true;
        }


        public virtual void UpdateStateMachine()
        {
            if (currentState != null)
            {
                currentState.UpdateState();
                currentState.HandleInput();

                OnStateUpdate?.Invoke(currentState.StateID);
            }

            else
            {
                Debug.LogWarning(name + " FSM Doesn't have a State");
                return;
            }
        }


        public virtual void ChangeState(AbstractStateBehaviour nextState)
        {
            if (currentState != null)
            {
                currentState.ExitState();
                previousState = currentState;
            }

            currentState = nextState;
            currentState.EnterState();

            if (log == true)
                Debug.Log("<color=yellow> Changed to state: </color> <color=cyan>" + currentState + "</color>");
        }


        public virtual void ChangeState(int stateId)
        {
            if (states != null && states.Length > 0)
                ChangeState(states[stateId]);
            else
                Debug.LogError("No state array found!");
        }


        public virtual void ChangeToPreviousState()
        {
            if (previousState != null)
            {
                currentState.ExitState();

                var _tempState = currentState;
                currentState = previousState;
                previousState = _tempState;
            }

            else
                Debug.Log("There is no previews state");
        }

        public void PauseState()
        {
            currentState.PauseState();
        }


        public virtual void GetStates()
        {
            if (parentStates != null && parentStates.childCount > 0)
            {
                states = Utilities.GetItems<AbstractStateBehaviour>(parentStates);

                if (Application.isPlaying)
                {
                    if (states != null && states.Length > 0)
                        for (int i = 0; i < states.Length; i++)
                            states[i].Initialize(this, i);
                }

                startingState = states[0];
            }

            else
                Debug.Log("No state parent!");
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (getStates)
                GetStates();
            getStates = false;

            if (states.Exists())
                stateIndex = Mathf.Clamp(stateIndex, 0, states.Length);

            if (changeState)
            {
                ChangeState(stateIndex);
                changeState = false;
            }
        }
#endif
    }
}
