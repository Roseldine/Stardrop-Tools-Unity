
using UnityEngine;

namespace StardropTools.UI.GenericButton
{
    [CreateAssetMenu(menuName = "Stardrop / UI / Button / Generic Button Graphics Database ")]
    public class UIGenericButtonStyleDBSO : ScriptableObject
    {
        [SerializeField] Sprite genericIcon;
        [SerializeField] TMPro.TMP_FontAsset genericFont;
        [Space]
        public UIGenericButtonGraphicCollection center;
        [Space]
        public UIGenericButtonGraphicCollection right;
        public UIGenericButtonGraphicCollection left;
        [Space]
        public UIGenericButtonGraphicCollection top;
        public UIGenericButtonGraphicCollection bottom;
    }
}

