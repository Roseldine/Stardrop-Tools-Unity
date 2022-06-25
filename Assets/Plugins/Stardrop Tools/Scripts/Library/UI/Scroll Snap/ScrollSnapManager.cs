
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.UI
{
    public class ScrollSnapManager : Singleton<ScrollSnapManager>
    {
        [UnityEngine.SerializeField] List<ScrollSnap> scrollSnaps;
        [UnityEngine.SerializeField] int lastIndex;
        [UnityEngine.SerializeField] float delayNullify = .5f;
        [SerializeField] bool getScrollSnaps;

        /// <summary>
        /// -1 = previous, 1 = next
        /// </summary>
        public void MoveNext(ScrollSnap snap, int direction)
        {
            int nextIndex = snap.index + direction;
            if (nextIndex < 0 || nextIndex >= scrollSnaps.Count)
                return;

            var id = snap.UniqueID;
            for (int i = 0; i < scrollSnaps.Count; i++)
                if (id == scrollSnaps[i].UniqueID)
                {
                    var s = scrollSnaps[i + direction];
                    s.MoveToIndex(s.currentIndex + direction);

                    break;
                }
        }

        public void SetCurrentScrollSnap(int index)
        {
            if (index != lastIndex)
                lastIndex = index;
        }

        public void AddToList(ScrollSnap snap)
        {
            if (scrollSnaps.Contains(snap) == false)
                scrollSnaps.Add(snap);

            SortListPriority();
        }

        public void RemoveFromList(ScrollSnap snap)
        {
            if (scrollSnaps.Contains(snap) == true)
                scrollSnaps.Remove(snap);
        }

        void SortListPriority()
        {
            ScrollSnap temp;
            int length = scrollSnaps.Count;

            for (int i = 0; i < length - 1; i++)
            {
                for (int j = 0; j < length - i - 1; j++)
                    if (scrollSnaps[j].priority < scrollSnaps[j + 1].priority)
                    {
                        // swap temp and arr[i]
                        temp = scrollSnaps[j];
                        scrollSnaps[j] = scrollSnaps[j + 1];
                        scrollSnaps[j + 1] = temp;
                    }
            }

            //scrollSnaps = Utilities.ReverseList(scrollSnaps);
        }

        private void OnValidate()
        {
            if (getScrollSnaps)
            {
                var snaps = FindObjectsOfType<ScrollSnap>();
                scrollSnaps = new List<ScrollSnap>();

                for (int i = 0; i < snaps.Length; i++)
                {
                    scrollSnaps.Add(snaps[i]);
                    snaps[i].index = i;
                }

                SortListPriority();

                getScrollSnaps = false;
            }
        }
    }
}