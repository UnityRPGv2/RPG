using System;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestList : MonoBehaviour
    {
        List<QuestStatus> questStatuses = new List<QuestStatus>();

        public Action onUpdate;

        public IEnumerable<QuestStatus> GetQuestStatuses()
        {
            return questStatuses;
        }

        public void AddQuest(Quest quest)
        {
            if (HasQuest(quest)) return;

            var questStatus = new QuestStatus(quest);
            questStatuses.Add(questStatus);
            if (onUpdate != null)
            {
                onUpdate();
            }
        }

        public void CompleteObjective(Quest quest, string objective)
        {
            foreach (var status in questStatuses)
            {
                if (status.GetQuest() == quest)
                {
                    status.CompleteObjective(objective);
                    if (status.IsComplete())
                    {
                        GiveReward(quest);
                    }
                }
            }
            if (onUpdate != null)
            {
                onUpdate();
            }
        }

        public bool HasQuest(Quest quest)
        {
            foreach (var status in questStatuses)
            {
                if (status.GetQuest() == quest)
                {
                    return true;
                }
            }
            return false;
        }

        private void GiveReward(Quest quest)
        {
            Inventory inv = GetComponent<Inventory>();
            foreach (var reward in quest.GetRewards())
            {
                if (!inv.AddToFirstEmptySlot(reward.item, reward.number))
                {
                    GetComponent<ItemDropper>().DropItem(reward.item, reward.number);
                }
            }
        }
    }
}