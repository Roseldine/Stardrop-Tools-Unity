
namespace StardropTools
{
    [System.Serializable]
    public class WeightedList<T>
    {
        public System.Collections.Generic.List<WeightedItem<T>> list = new System.Collections.Generic.List<WeightedItem<T>>();

        public int Count { get => list.Count; }

        public void Add(T item, float weight)
            => list.Add(new WeightedItem<T>(item, weight));

        public void Add(WeightedItem<T> item)
        {
            if (list.Contains(item) == false)
                list.Add(item);
        }

        public void Remove(WeightedItem<T> item)
        {
            if (list.Contains(item) == false)
                list.Remove(item);
        }

        public T GetRandom()
        {
            if (list.Count == 0)
            {
                UnityEngine.Debug.Log("List is empty!");
                return default(T);
            }

            float totalWeight = 0;

            foreach (WeightedItem<T> item in list)
                totalWeight += item.weight;            

            float value = UnityEngine.Random.value * totalWeight;

            float sumWeight = 0;

            foreach (WeightedItem<T> item in list)
            {
                sumWeight += item.weight;

                if (sumWeight >= value)
                    return item.item;
            }

            return default(T);
        }
    }
}