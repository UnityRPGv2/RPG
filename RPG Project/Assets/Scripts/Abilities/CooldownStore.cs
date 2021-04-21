using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Abilities
{
    public class CooldownStore : MonoBehaviour
    {
        Dictionary<InventoryItem, float> timers = new Dictionary<InventoryItem, float>();
        Dictionary<InventoryItem, float> initialTime = new Dictionary<InventoryItem, float>();

        public void StartCooldown(InventoryItem item, float time)
        {
            initialTime[item] = time;
            timers[item] = Time.time + time;
        }

        public float GetTimeRemaining(InventoryItem item)
        {
            if (item == null) return 0;
            if (!timers.ContainsKey(item)) return 0;
            float remaining = timers[item] - Time.time;
            if (remaining < 0)
            {
                remaining = 0;
                timers.Remove(item);
                initialTime.Remove(item);
            }
            return remaining;
        }

        public float GetFractionRemaining(InventoryItem item)
        {
            float remainingTime = GetTimeRemaining(item);
            if (remainingTime <= 0)
            {
                return 0;
            }
            return remainingTime / initialTime[item];
        }
    }
}