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
        [SerializeField] Reward[] rewards;

        [System.Serializable]
        public struct Objective
        {
            public string reference;
            public string description;
        }

        [System.Serializable]
        public struct Reward
        {
            public int number;
            public InventoryItem item;
        }

        public static Quest GetByName(string name)
        {
            foreach (var quest in Resources.LoadAll<Quest>(""))
            {
                if (quest.name == name)
                {
                    return quest;
                }
            }
            return null;
        }

        public string GetTitle()
        {
            return name;
        }

        public int GetObjectivesNum()
        {
            return objectives.Length;
        }

        public IEnumerable<Reward> GetRewards()
        {
            return rewards;
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