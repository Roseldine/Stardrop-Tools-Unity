
namespace StardropTools.UI
{
    public class UISizeCopy : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] string identifier;
        [UnityEngine.SerializeField] UnityEngine.RectTransform target;
        [UnityEngine.SerializeField] System.Collections.Generic.List<UnityEngine.RectTransform> list;
        [UnityEngine.SerializeField] bool copy;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] bool getChildren;
        [UnityEngine.SerializeField] bool clear;

        public void SetTarget(UnityEngine.RectTransform target)
            => this.target = target;

        public void AddToCopyList(UnityEngine.RectTransform rect, bool copy = false)
        {
            if (rect == null)
                return;

            if (list.Contains(rect) == false)
                list.Add(rect);

            if (copy)
                Copy();
        }

        public void Copy()
        {
            if (target != null)
            {
                if (getChildren)
                    list = Utilities.GetItems<UnityEngine.RectTransform>(target);

                if (list.Count > 0)
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].sizeDelta = target.sizeDelta;
                        //list[i].sizeDelta = target.rect.size;

                        //var rect = target.rect;
                        //list[i].rect.Set(rect.x, rect.y, rect.width, rect.height);
                    }

                UnityEngine.Debug.Log("Copied");
            }
        }

        private void OnValidate()
        {
            if (copy)
                Copy();
            copy = false;

            if (clear)
                list = null;
            clear = false;

            if (getChildren)
            {
                list = Utilities.GetItems<UnityEngine.RectTransform>(target);
                getChildren = false;
            }
        }
    }
}