
using UnityEngine;

namespace StardropTools.UI
{
    [System.Serializable]
    public class UIColorContrast : MonoBehaviour
    {
        [SerializeField] Color colorLight = Color.white;
        [SerializeField] Color colorDark = Color.white;
        [Space]
        [SerializeField] UnityEngine.UI.Image[] imgsLight;
        [SerializeField] UnityEngine.UI.Image[] imgsDark;
        [Space]
        [Min(0)][SerializeField] float pixelPerUnit = 32;
        [SerializeField] bool validate = true;

        // light
        public void UpdateColorsLight()
        {
            if (imgsLight != null && imgsLight.Length > 0)
                for (int i = 0; i < imgsLight.Length; i++)
                    if (imgsLight[i] != null)
                    {
                        imgsLight[i].color = colorLight;
                        imgsLight[i].pixelsPerUnitMultiplier = pixelPerUnit;
                    }
        }

        // dark
        public void UpdateColorsDark()
        {
            if (imgsDark != null && imgsDark.Length > 0)
                for (int i = 0; i < imgsDark.Length; i++)
                    if (imgsDark[i] != null)
                    {
                        imgsDark[i].color = colorDark;
                        imgsDark[i].pixelsPerUnitMultiplier = pixelPerUnit;
                    }
        }

        private void OnValidate()
        {
            if (validate)
            {
                UpdateColorsLight();
                UpdateColorsDark();
            }
        }
    }
}
