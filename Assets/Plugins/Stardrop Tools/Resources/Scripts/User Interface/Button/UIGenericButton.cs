
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.UI.GenericButton
{
    public class UIGenericButton : UIButton
    {
        public enum GraphicOptions { icon, text, both }

        [Header("Generic Buttons")]
        [SerializeField] RectTransformObjectData.OrientationEnum orientation;
        [SerializeField] GraphicOptions graphicOptions;
        [Space]
        [SerializeField] UIGenericButtonComponentData componentData;
        [SerializeField] UIGenericButtonGraphicData graphicData;
        [Space]
        [SerializeField] UIGenericButtonStyleDBSO[] spriteDB;
        [SerializeField] int styleID;

        protected void ValidateData()
        {
            // orientation
            if (orientation != RectTransformObjectData.OrientationEnum.none && DataLayout != RectTransformObjectData.LayoutOptions.orientation)
                DataLayout = RectTransformObjectData.LayoutOptions.orientation;

            if (DataOrientation != orientation)
                DataOrientation = orientation;

            // set icon or text
            if (graphicOptions == GraphicOptions.icon)
            {
                componentData.SetIconActive(true);
                componentData.SetTextActive(false);
            }

            else if (graphicOptions == GraphicOptions.text)
            {
                componentData.SetIconActive(false);
                componentData.SetTextActive(true);
            }

            else if (graphicOptions == GraphicOptions.both)
            {
                componentData.SetIconActive(true);
                componentData.SetTextActive(true);
            }


            // set component data
            if (componentData.background != null)
                componentData.SetBackground(graphicData.background);

            if (componentData.gradient != null)
                componentData.SetGradient(graphicData.gradient);

            if (componentData.shade != null)
                componentData.SetShade(graphicData.shade);

            if (componentData.outline != null)
                componentData.SetOutline(graphicData.outline);

            if (componentData.icon != null)
                componentData.SetIcon(graphicData.icon);

            if (componentData.text != null)
                componentData.SetText(graphicData.text);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            ValidateData();
        }
    }

    [System.Serializable]
    public class UIGenericButtonComponentData
    {
        public Image background;
        public Image gradient;
        public Image shade;
        public Image outline;
        public Image icon;
        public TMPro.TextMeshProUGUI text;

        private GameObject iconObj, textObj;

        public void SetBackground(Sprite sprite)
        {
            if (sprite != null && background != null && background.sprite != sprite)
                background.sprite = sprite;
        }

        public void SetGradient(Sprite sprite)
        {
            if (sprite != null && gradient != null && gradient.sprite != sprite)
                gradient.sprite = sprite;
        }

        public void SetShade(Sprite sprite)
        {
            if (sprite != null && shade != null && shade.sprite != sprite)
                shade.sprite = sprite;
        }

        public void SetOutline(Sprite sprite)
        {
            if (sprite != null && outline != null && outline.sprite != sprite)
                outline.sprite = sprite;
        }

        public void SetIcon(Sprite sprite)
        {
            if (sprite != null && icon != null && icon.sprite != sprite)
                icon.sprite = sprite;
        }

        public void SetText(string value)
        {
            if (value != null && text != null && text.text != value)
                text.text = value;
        }

        public void SetIconActive(bool value)
        {
            if (icon == null)
                return;

            if (iconObj == null)
                iconObj = icon.gameObject;

            iconObj.SetActive(value);
        }

        public void SetTextActive(bool value)
        {
            if (text == null)
                return;

            if (textObj == null)
                textObj = text.gameObject;

            textObj.SetActive(value);
        }
    }

    [System.Serializable]
    public class UIGenericButtonGraphicData
    {
        public Sprite background;
        public Sprite gradient;
        public Sprite shade;
        public Sprite outline;
        public Sprite icon;
        public string text;
    }
}

