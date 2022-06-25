
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenCluster : BaseComponent
    {
        [SerializeField] bool testTweens;
        [Space]
        [SerializeField] Transform[] parentTweens;
        [SerializeField] TweenComponent[] tweens;
        [SerializeField] float duration;
        [SerializeField] bool getTweens;

        float time;

        public readonly CoreEvent OnTweenStart = new CoreEvent();
        public readonly CoreEvent OnTweenComplete = new CoreEvent();

        protected override void OnDisable()
        {
            base.OnDisable();
            StopTweens();
        }

        public void StartTweens()
        {
            time = 0;
            StopTweens();

            for (int i = 0; i < tweens.Length; i++)
                tweens[i].InitializeTween();

            OnTweenStart?.Invoke();

            LoopManager.OnUpdate.AddListener(WaitCompletion);
        }

        public void PauseTweens()
        {
            for (int i = 0; i < tweens.Length; i++)
                tweens[i].PauseTween();
        }

        public void StopTweens()
        {
            for (int i = 0; i < tweens.Length; i++)
                tweens[i].StopTween();
        }

        void WaitCompletion()
        {
            time += Time.deltaTime;
            if (time > duration)
            {
                OnTweenComplete?.Invoke();

                time = 0;
                //Debug.Log("Cluster Complete!");
                LoopManager.OnUpdate.RemoveListener(WaitCompletion);
            }
        }

        void GetTweens()
        {
            System.Collections.Generic.List<TweenComponent> list = new System.Collections.Generic.List<TweenComponent>();

            for (int p = 0; p < parentTweens.Length; p++)
            {
                // get components from parents themselfs
                var components = parentTweens[p].GetComponents<TweenComponent>();
                if (components.Exists())
                {
                    for (int i = 0; i < components.Length; i++)
                        list.Add(components[i]);
                }

                // get components from children
                components = Utilities.GetItems<TweenComponent>(parentTweens[p]).ToArray();
                if (components.Exists())
                {
                    for (int i = 0; i < components.Length; i++)
                        list.Add(components[i]);
                }
            }

            tweens = list.ToArray();

            if (tweens.Exists())
            {
                duration = 0;

                for (int i = 0; i < tweens.Length; i++)
                {
                    var tween = tweens[i];
                    tween.SetTweenID(i);

                    float tweenDuration = tween.Duration + tween.Delay;
                    if (tweenDuration > duration)
                        duration = tweenDuration;
                }
            }
        }

        private void OnValidate()
        {
            if (getTweens)
                GetTweens();
            getTweens = false;

            if (testTweens)
                StartTweens();
            testTweens = false;
        }
    }
}