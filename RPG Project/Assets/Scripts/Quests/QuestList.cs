using System;
using System.Collections.Generic;
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
    }
}