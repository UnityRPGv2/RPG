using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "RPG/Quest", order = 0)]
public class Quest : ScriptableObject {
    [SerializeField] Objective[] objectives;
    [SerializeField] InventoryItem[] rewards;

    [System.Serializable]
    struct Objective
    {
        public bool complete;
        public string description;
    }

    public string GetTitle()
    {
        return name;
    }

    public int GetObjectivesNum()
    {
        return objectives.Length;
    }

    public IEnumerable<string> GetCompletedObjectives()
    {
        foreach (var objective in objectives)
        {
            if (objective.complete)
            {
                yield return objective.description;
            }
        }
    }

    public IEnumerable<string> GetOutstandingObjectives()
    {
        foreach (var objective in objectives)
        {
            if (!objective.complete)
            {
                yield return objective.description;
            }
        }
    }

    public IEnumerable<string> GetRewardNames()
    {
        foreach (var reward in rewards)
        {
            yield return reward.GetDisplayName();
        }
    }
}