using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "RPG/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] Objective[] objectives;
        [SerializeField] InventoryItem[] rewards;

        [System.Serializable]
        public struct Objective
        {
            public string reference;
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

        public IEnumerable<string> GetRewardNames()
        {
            foreach (var reward in rewards)
            {
                yield return reward.GetDisplayName();
            }
        }

        public IEnumerable<Objective> GetObjectives()
        {
            return (IEnumerable<Objective>) objectives;
        }

        public bool HasObjective(string testObjective)
        {
            foreach (var objective in objectives)
            {
                if (objective.reference == testObjective)
                {
                    return true;
                }
            }
            return false;
        }
    }
}