
using UnityEngine;

namespace StardropTools.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class UIButton : CoreRectTransform
    {
        [Header("Buttons")]
        [SerializeField] int buttonID;
        [SerializeField] protected UnityEngine.UI.Button button;

        public int ButtonID { get => buttonID; set => buttonID = value; }
        public UnityEngine.UI.Button Button { get => button; }


        public CoreEvent OnClick = new CoreEvent();
        public CoreEvent<UIButton> OnClickButton = new CoreEvent<UIButton>();
        public CoreEvent<int> OnClickID = new CoreEvent<int>();

        public override void Initialize()
        {
            base.Initialize();
            SubscribeToEvents();
        }

        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            button.onClick.AddListener(Pressed);
        }

        public virtual void Pressed()
        {
            if (IsInitialized == false)
                return;

            OnClick?.Invoke();
            OnClickButton?.Invoke(this);
            OnClickID?.Invoke(buttonID);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (button == null && GetComponent<UnityEngine.UI.Button>() != null)
                button = GetComponent<UnityEngine.UI.Button>();
        }
    }
}