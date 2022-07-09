
namespace StardropTools.Ability
{
    [UnityEngine.CreateAssetMenu(menuName = "Settings / Ability Layer Mask")]
    public class AbilityLayerMask : UnityEngine.ScriptableObject
    {
        [UnityEngine.SerializeField] UnityEngine.LayerMask layerMask;
        public UnityEngine.LayerMask LayerMask { get => layerMask; }
    }
}