
using UnityEngine;

namespace StardropTools.FSM.StateMachineSO
{
    public class FiniteStateMachineSO : MonoBehaviour
    {
        public int stateMachineID;

        [Header("State Viewer")]
        [SerializeField] protected AbstractStateSO startState;
        [SerializeField] protected AbstractStateSO currentState;
        [SerializeField] protected AbstractStateSO previousState;
        [Space]
        [SerializeField] protected float timeInState;

        [Header("States")]
        [SerializeField] protected AbstractStateSO[] states;
        [SerializeField] protected bool debug;

        public virtual void Initialize()
        {
            if (startState != null)
                ChangeState(startState);
        }


        public virtual void UpdateStateMachine()
        {
            if (currentState != null)
            {
                currentState.UpdateState(timeInState);
                currentState.UpdateInput(timeInState);

                timeInState += Time.deltaTime;
            }

            else
                Debug.LogWarning("FSM of: " + name + ", doesn't have a State");
        }


        public virtual void ChangeState(AbstractStateSO nextState)
        {
            if (currentState != null && currentState != nextState)
            {
                currentState.ExitState();
                previousState = currentState;
            }

            timeInState = 0;

            currentState = nextState;
            currentState.EnterState();

            if (debug)
                Debug.Log("<color=yellow> Changed to state: </color> <color=cyan>" + currentState + "</color>");
        }

        public virtual void ChangeState(int nextStateID)
        {
            if (states != null && states.Length > 0)
                ChangeState(states[nextStateID]);
            else
                Debug.LogError("No state array found!");
        }

        public virtual void ChangeToPreviewsState()
        {
            if (previousState != null)
            {
                currentState.ExitState();

                var _tempState = currentState;
                currentState = previousState;
                previousState = _tempState;
            }

            else
                Debug.LogWarning("There is no previews state");
        }

        public virtual void ResetToStartState()
        {
            currentState.ExitState();
            previousState = currentState;

            timeInState = 0;

            currentState = startState;
            currentState.EnterState();
        }
    }
}