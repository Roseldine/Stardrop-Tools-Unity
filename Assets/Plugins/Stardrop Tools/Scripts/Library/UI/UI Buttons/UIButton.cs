
using UnityEngine;

namespace StardropTools.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class UIButton : BaseUIObject
    {
        [Header("Buttons")]
        [SerializeField] int buttonID;
        [SerializeField] protected UnityEngine.UI.Button button;

        public int ButtonID { get => buttonID; set => buttonID = value; }
        public UnityEngine.UI.Button Button { get => button; }


        public readonly BaseEvent OnClick = new BaseEvent();
        public readonly BaseEvent<UIButton> OnClickButton = new BaseEvent<UIButton>();
        public readonly BaseEvent<int> OnClickID = new BaseEvent<int>();

        public override void Initialize()
        {
            base.Initialize();
            SubscribeToEvents();
        }

        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            button.onClick.AddListener(Pressed);
            button.onClick.AddListener(TestDebug);
        }

        public virtual void Pressed()
        {
            if (IsInitialized == false)
                return;

            OnClick?.Invoke();
            OnClickButton?.Invoke(this);
            OnClickID?.Invoke(buttonID);

            TestDebug();
        }

        void TestDebug()
        {
            if (coreData.Debug)
                Debug.Log("Btn clicked!");
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (button == null && GetComponent<UnityEngine.UI.Button>() != null)
                button = GetComponent<UnityEngine.UI.Button>();
        }
    }
}
