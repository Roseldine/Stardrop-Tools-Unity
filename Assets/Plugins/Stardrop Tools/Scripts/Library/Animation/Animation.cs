
using UnityEngine;

namespace StardropTools
{
    public class Animation : CoreComponent
    {
        [Header("Animation")]
        [SerializeField] Animator animator;
        [SerializeField] AnimState[] animStates;
        [Min(0)] [SerializeField] float overralCrossFadeTime = 0;
        [SerializeField] float animTime;
        [Space]
        [SerializeField] bool getAnimStates;
        [SerializeField] bool clearAnimStates;

        Coroutine animTimeCR;

        public AnimState CurrentState { get => animStates[currentAnimID]; }
        public int currentAnimID;

        public readonly CoreEvent<int> OnAnimStartID = new CoreEvent<int>();
        public readonly CoreEvent<int> OnAnimCompleteID = new CoreEvent<int>();
        public readonly CoreEvent<int> OnAnimEventID = new CoreEvent<int>();

        public readonly CoreEvent<AnimState> OnAnimStart = new CoreEvent<AnimState>();
        public readonly CoreEvent<AnimState> OnAnimComplete = new CoreEvent<AnimState>();
        public readonly CoreEvent<AnimState> OnAnimEvent = new CoreEvent<AnimState>();

        AnimatorStateInfo stateInfo;

        // WORK IN PROGRESS
        public void AnimEventDetection()
        {
            // check if state has any Events
            if (CurrentState.EventCount > 0)
            {
                if (stateInfo.Equals(animator.GetCurrentAnimatorStateInfo(0)) == false)
                    stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                animTime = stateInfo.normalizedTime;

                for (int i = 0; i < CurrentState.EventCount; i++)
                    CurrentState.AnimEvents[i].EventCheck(MathUtility.RoundDecimals(animTime, 1));

                if (animTime > .8f && Mathf.Approximately(MathUtility.RoundDecimals(animTime, 3), .999f))
                    for (int i = 0; i < CurrentState.EventCount; i++)
                        CurrentState.AnimEvents[i].ResetInvoke();
            }
        }


        public void RefreshAnimState()
        {
            if (animator.GetCurrentAnimatorStateInfo(CurrentState.Layer).IsName(CurrentState.StateName) == false)
            {
                animator.Play(CurrentState.StateName, CurrentState.Layer);
                //Debug.Log("Refeshed Anim!");
            }
        }

        
        public void PlayAnimation(int animID, bool forceAnimation = false, bool disableOnFinish = false)
        {
            if (forceAnimation == false && animID == currentAnimID)
                return;

            if (animID >= 0 && animID < animStates.Length)
            {
                var targetState = animStates[animID];

                if (animator.enabled == false)
                    animator.enabled = true;

                animator.Play(targetState.StateName, targetState.Layer);
                RefreshAnimState();

                AnimationLifetime(targetState.LengthInSeconds, !disableOnFinish);
                currentAnimID = animID;

                OnAnimStartID?.Invoke(currentAnimID);
                OnAnimStart?.Invoke(CurrentState);
            }

            else
                Debug.LogFormat("Animation ID: {0}, does not exist", animID);
        }

        
        public void CrossFadeAnimation(int animID, bool forceAnimation = false, bool disableOnFinish = false)
        {
            if (forceAnimation == false && animID == currentAnimID)
                return;

            if (animID >= 0 && animID < animStates.Length)
            {
                var targetState = animStates[animID];

                if (animator.enabled == false)
                    animator.enabled = true;

                animator.CrossFade(targetState.StateName, targetState.crossfade);

                AnimationLifetime(targetState.LengthInSeconds, !disableOnFinish);
                currentAnimID = animID;

                OnAnimStartID?.Invoke(currentAnimID);
                OnAnimStart?.Invoke(CurrentState);
            }

            else
                Debug.LogFormat("Animation ID: {0}, does not exist", animID);
        }

        void AnimationLifetime(float time, bool disableOnFinish)
        {
            if (isActiveAndEnabled == false)
                return;

            if (animTimeCR != null)
                StopCoroutine(animTimeCR);

            animTimeCR = StartCoroutine(AnimTimeCR(time, disableOnFinish));
        }

        System.Collections.IEnumerator AnimTimeCR(float time, bool disableOnFinish)
        {
            yield return WaitForSecondsManager.GetWait("animation", time);
            animator.enabled = disableOnFinish;
            OnAnimCompleteID?.Invoke(currentAnimID);
            OnAnimComplete?.Invoke(CurrentState);
        }

#if UNITY_EDITOR
        void GetAnimatorStates()
        {
            if (animator != null)
            {
                // Get a reference to the Animator Controller:
                UnityEditor.Animations.AnimatorController animController = animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;

                // Number of layers:
                int layerCount = animController.layers.Length;
                Debug.Log("Layer Count: " + layerCount);
                AnimationClip[] animClips = animator.runtimeAnimatorController.animationClips;
                Debug.Log("Clip count: " + animClips.Length);

                if (animClips.Length == 0)
                {
                    Debug.Log("NO Animation Clips!");
                    return;
                }

                var listAnimStates = new System.Collections.Generic.List<AnimState>();

                for (int layer = 0; layer < layerCount; layer++)
                {
                     // Names of each layer:
                     //Debug.LogFormat("Layer {0}: {1}", layer, animController.layers[layer].name);

                    UnityEditor.Animations.AnimatorStateMachine animatorStateMachine = animController.layers[layer].stateMachine;
                    UnityEditor.Animations.ChildAnimatorState[] states = animatorStateMachine.states;

                    // loop through ChildAnimatorState && add to list
                    for (int i = 0; i < states.Length; i++)
                    {
                        var newState = new AnimState(states[i].state.name, layer, .15f, animClips[i].length);
                        listAnimStates.Add(newState);
                        Debug.Log("State: " + states[i].state.name);
                    }
                }

                animStates = listAnimStates.ToArray();
            }

            else
                Debug.Log("Animator not found!");
        }


        private void OnValidate()
        {
            if (animator == null)
                animator = GetComponent<Animator>();

            if (getAnimStates)
            {
                GetAnimatorStates();
                getAnimStates = false;
            }

            if (clearAnimStates)
            {
                animStates = new AnimState[0];
                clearAnimStates = false;
            }

            if (overralCrossFadeTime > 0)
                foreach (AnimState animeState in animStates)
                    animeState.crossfade = overralCrossFadeTime;
        }
#endif
    }
}

