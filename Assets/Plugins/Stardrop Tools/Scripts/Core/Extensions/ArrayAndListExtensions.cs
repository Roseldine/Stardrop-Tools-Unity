
using System.Collections.Generic;
using UnityEngine;

public static class ArrayAndListExtensions
{
    #region Array Extensions
    public static bool Exists<T>(this T[] array)
    {
        if (array != null && array.Length > 0)
            return true;
        else
            return false;
    }


    public static bool ContainsObject(this Object[] array, Object obj)
    {
        if (Exists(array))
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == obj)
                    return true;
            }

            return false;
        }

        else
        {
            Debug.Log("Array doesn't exist");
            return false;
        }
    }

    /// <summary>
    /// Find and empty spot on array and fill it with selected element
    /// </summary>
    public static bool Add<T>(this T[] array, T element)
    {
        if (Exists(array))
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                {
                    array[i] = element;
                    return true;
                }
            }

            return false;
        }

        else
            return false;
    }


    public static int GetEmptyIndex<T>(this T[] array)
    {
        if (array.Exists())
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                    return i;
            }

            return -1;
        }

        else
            return -1;
    }

    public static T GetRandom<T>(this T[] array)
        => array[Random.Range(0, array.Length)];
    #endregion // arrays


    #region List Extensions
    public static bool Exists<T>(this List<T> list)
    {
        if (list != null && list.Count > 0)
            return true;
        else
            return false;
    }

    public static bool AddSafe<T>(this List<T> list, T element)
    {
        if (list.Exists() && list.Contains(element) == false)
        {
            list.Add(element);
            return true;
        }

        else
            return false;
    }

    public static void AddArrayToList<T>(this List<T> listToAdd, T[] array)
    {
        foreach (T item in array)
            listToAdd.AddSafe(item);
    }

    public static T GetRandom<T>(this List<T> list)
        => list[Random.Range(0, list.Capacity)];

    #endregion // lists
}