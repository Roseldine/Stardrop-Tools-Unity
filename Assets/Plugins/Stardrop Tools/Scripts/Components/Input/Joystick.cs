using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StardropTools.UserInput
{
    public class Joystick : RectTransformObject, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public enum TargetAxis { both, horizontal, vertical }

        [SerializeField] protected JoystickPart background;
        [SerializeField] protected JoystickPart handle;

        Vector2 startInput;
        Vector2 currentInput;
        Vector2 previousInput;

        protected Canvas canvas;
        protected new Camera camera;

        public float Horizontal { get; protected set; }
        public float Vertical { get; protected set; }
        public Vector2 Direction { get => new Vector2(Horizontal, Vertical).normalized; }
        public Vector3 DirectionXZ { get => new Vector3(Horizontal, 0, Vertical).normalized; }

        public override void Initialize()
        {
            base.Initialize();

            canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
                Debug.LogError("The Joystick is not placed inside a canvas");
        }

        public virtual void UpdateJoystick()
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            startInput = Input.mousePosition;
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            currentInput = Input.mousePosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            previousInput = currentInput;
            currentInput = Vector3.zero;
        }
    }

    [System.Serializable]
    public class JoystickPart
    {
        public UnityEngine.UI.Image image;
        public RectTransform rectTransform;
        public GameObject gameObject;

        public void SetActive(bool value) => gameObject.SetActive(value);
        public void SetAnchoredPosition(Vector2 position) => rectTransform.anchoredPosition = position;
    }
}