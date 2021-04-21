using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Abilities
{
    public class CooldownStore : MonoBehaviour
    {
        Dictionary<InventoryItem, float> timers = new Dictionary<InventoryItem, float>();

        public void StartCooldown(InventoryItem item, float time)
        {
            timers[item] = Time.time + time;
            Debug.Log(timers);
        }

        public float GetTimeRemaining(InventoryItem item)
        {
            if (!timers.ContainsKey(item)) return 0;
            float remaining = timers[item] - Time.time;
            if (remaining < 0)
            {
                remaining = 0;
                timers.Remove(item);
            }
            return remaining;
        }
    }
}