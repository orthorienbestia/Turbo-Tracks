using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

// ReSharper disable once CheckNamespace
namespace _Project.Scripts.Utility
{
    public static class ListExtensions
    {
        /// <summary>
        /// Returns random object from the list.
        /// </summary>
        public static T GetRandomItem<T>(this List<T> list)
        {
            if (list.Count == 0)
            {
                throw new Exception("List is EMPTY. Cannot get random item.");
            }

            return list[Random.Range(0, list.Count)];
        }
    }
}