
using UnityEngine;

namespace StardropTools.UI
{
    public class UISizeDivide : MonoBehaviour
    {
        public enum OrientationEnum { vertical, horizontal }

        [SerializeField] OrientationEnum orientation;
        [SerializeField] RectTransform parentRect;
        [SerializeField] UIDivideElement[] elements;
        [SerializeField] bool getElements;
        [Space]
        [SerializeField] bool divide;
        [SerializeField] bool uniform;
        [SerializeField] bool validate;

        void Divide()
        {
            if (elements == null && elements.Length <= 1)
            {
                Debug.Log("Not enought elements to divide");
                return;
            }

            if (uniform)
            {
                float div = 1f / elements.Length;
                float divIncrement = div;

                for (int i = 0; i < elements.Length; i++)
                {
                    elements[i].Percent = divIncrement;
                    divIncrement += div;

                    if (i == elements.Length - 1)
                        elements[i].Percent = 1;
                }
            }

            if (elements.Length == 2)
                DivideTwo();

            if (elements.Length > 2)
                DivideMore();

            foreach (var element in elements)
                element.UpdateSize();
        }

        void DivideTwo()
        {
            elements[0].Rect.sizeDelta = CalculateSize(elements[0]);

            // get remaining percent
            elements[1].Percent = 1 - elements[0].Percent;
            elements[1].Rect.sizeDelta = CalculateSize(elements[1]);
        }

        void DivideMore()
        {

        }

        Vector2 CalculateSize(UIDivideElement element)
        {
            Vector2 size = parentRect.sizeDelta;

            if (orientation == OrientationEnum.horizontal)
                return new Vector2(size.x * element.Percent, size.y);
            else if (orientation == OrientationEnum.vertical)
                return new Vector2(size.x, size.y * element.Percent);
            else
                return size * element.Percent;
        }

        void GetElements()
        {
            if (parentRect == null)
                return;

            var rects = Utilities.GetItems<RectTransform>(parentRect);

            elements = new UIDivideElement[rects.Length];
            float div = 1f / rects.Length;
            float divIncrement = div;

            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = new UIDivideElement(rects[i], divIncrement);
                divIncrement += div;

                if (i == elements.Length - 1)
                    elements[i].Percent = 1;
            }
        }

        private void OnValidate()
        {
            if (getElements)
                GetElements();
            getElements = false;

            if (divide || uniform)
                Divide();
            divide = false;
            uniform = false;

            if (validate)
                Divide();
        }
    }

    [System.Serializable]
    public class UIDivideElement
    {
        public UIDivideElement(RectTransform rect, float percent)
        {
            Rect = rect;
            Percent = percent;
        }

        public RectTransform Rect;
        public Vector2 size;
        [Range(0, 1)] public float Percent;

        public void UpdateSize() => size = Rect.sizeDelta;
    }
}