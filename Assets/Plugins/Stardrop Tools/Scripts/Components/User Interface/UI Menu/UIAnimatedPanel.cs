
using UnityEngine;

namespace StardropTools.UI
{
    public class UIAnimatedPanel : RectTransformObject
    {
        [SerializeField] protected new UITweenAnimation animation;
        [SerializeField] protected UIButton[] closeButtons;

        public override void Initialize()
        {
            base.Initialize();

            InitializeCloseButtons();
            SubscribeToEvents();
        }

        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            animation.OnOpen.AddListener(() => SetActive(true));

            foreach (UIButton btn in closeButtons)
                btn.OnClick.AddListener(Close);
        }

        protected void InitializeCloseButtons()
        {
            if (IsInitialized)
                return;

            foreach (UIButton btn in closeButtons)
                btn.Initialize();
        }

        public virtual void Open()
        {
            if (animation != null)
                animation.Open();
        }

        public virtual void Close()
        {
            if (animation != null)
                animation.Close();
        }
    }
}