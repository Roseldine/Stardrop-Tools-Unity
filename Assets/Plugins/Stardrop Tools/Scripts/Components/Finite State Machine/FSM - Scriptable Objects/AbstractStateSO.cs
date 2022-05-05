
using UnityEngine;

namespace StardropTools.FSM.StateMachineSO
{
    public abstract class AbstractStateSO : ScriptableObject
    {
        public int stateID;

        public virtual bool EnterState()
        {

            return true;
        }

        public virtual bool ExitState()
        {

            return true;
        }

        public virtual bool UpdateState(float timeInState)
        {

            return true;
        }

        public virtual bool UpdateInput(float timeInState)
        {

            return true;
        }
    }
}