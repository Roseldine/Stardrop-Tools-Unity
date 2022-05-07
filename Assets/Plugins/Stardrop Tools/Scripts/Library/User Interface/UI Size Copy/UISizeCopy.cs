
namespace StardropTools.UI
{
    public class UISizeCopy : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] string identifier;
        [UnityEngine.SerializeField] UnityEngine.RectTransform target;
        [UnityEngine.SerializeField] UnityEngine.RectTransform[] copys;
        [UnityEngine.SerializeField] bool updateName;
        [UnityEngine.SerializeField] bool copy;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] bool getChildren;
        [UnityEngine.SerializeField] bool clear;

        public void Copy()
        {
            if (target != null)
            {
                if (getChildren)
                    copys = Utilities.GetItems<UnityEngine.RectTransform>(target);

                if (copys != null && copys.Length > 0)
                    for (int i = 0; i < copys.Length; i++)
                        copys[i].sizeDelta = target.sizeDelta;
            }
        }

        public void SetName()
        {
            if (UnityEngine.Application.isPlaying)
                return;
            if (identifier != null && identifier.Length > 0)
                name = "Size Copy - " + identifier;
        }

        private void OnValidate()
        {
            if (copy)
                Copy();
            copy = false;

            if (updateName)
                SetName();
            updateName = false;

            if (clear)
                copys = null;
            clear = false;
        }
    }
}