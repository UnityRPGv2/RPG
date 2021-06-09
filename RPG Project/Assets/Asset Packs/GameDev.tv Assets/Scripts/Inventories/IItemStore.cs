using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameDevTV.Inventories
{
    public static class ItemStore
    {
        public static void GiveItem(GameObject receiver, InventoryItem item, int number)
        {
            GiveItems(receiver, new []{(item, number)});
        }

        public static void GiveItems(GameObject receiver, IEnumerable<(InventoryItem, int)> items)
        {
            foreach (var (store, i, n) in CanAnyAccept(receiver, items, out _))
            {
                store.AddItems(i, n);
            }
        }

        public static bool CanAccept(GameObject receiver, InventoryItem item, int number)
        {
            return CanAccept(receiver, new[] { (item, number) });
        }

        public static bool CanAccept(GameObject receiver, IEnumerable<(InventoryItem, int)> items)
        {
            IEnumerable<(IItemStore, InventoryItem, int)> pairs = CanAnyAccept(receiver, items, out var remainder);
            return remainder.Count == 0;
        }

        private static IEnumerable<(IItemStore, InventoryItem, int)> CanAnyAccept(GameObject receiver, IEnumerable<(InventoryItem, int)> items, out IDictionary<InventoryItem, int> remainder)
        {
            var result = new List<(IItemStore, InventoryItem, int)>();
            remainder = ToDict(items);
            foreach (var store in GetSortedStores(receiver))
            {
                foreach (var (item, num) in store.CanAcceptItems(ToList(remainder)))
                {
                    remainder[item] -= num;
                    if (remainder[item] == 0) remainder.Remove(item);
                    result.Add((store, item, num));
                }
            }
            return result;
        }

        private static IEnumerable<IItemStore> GetSortedStores(GameObject receiver)
        {
            return receiver.GetComponents<IItemStore>().OrderByDescending((i) => i.GetPriority());
        }

        private static IEnumerable<(InventoryItem, int)> ToList(IDictionary<InventoryItem, int> dict)
        {
            var list = new List<(InventoryItem, int)>();
            foreach (var kvpair in dict)
            {
                if (kvpair.Value > 0)
                {
                    list.Add((kvpair.Key, kvpair.Value));
                }
            }
            return list;
        }

        private static Dictionary<InventoryItem, int> ToDict(IEnumerable<(InventoryItem, int)> list)
        {
            var dict = new Dictionary<InventoryItem, int>();
            foreach (var pair in list)
            {
                if (pair.Item2 <= 0) continue;
                dict[pair.Item1] = pair.Item2;
            }
            return dict;
        }
    }

    public interface IItemStore {

        void AddItems(InventoryItem item, int number);
        IEnumerable<(InventoryItem, int)> CanAcceptItems(IEnumerable<(InventoryItem, int)> items);
        int GetPriority();
    }
}