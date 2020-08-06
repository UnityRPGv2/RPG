using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestStatus
    {
        Quest quest;
        List<string> completedObjectives = new List<string>();

        public QuestStatus(Quest quest)
        {
            this.quest = quest;
        }

        public IEnumerable<string> GetCompletedObjectives()
        {
            foreach (var objective in quest.GetObjectives())
            {
                if (completedObjectives.Contains(objective.reference))
                {
                    yield return objective.description;
                }
            }
        }

        public int GetCompletedCount()
        {
            int count = 0;
            foreach (var objective in quest.GetObjectives())
            {
                if (completedObjectives.Contains(objective.reference))
                {
                    count++;
                }
            }
            return count;
        }

        public Quest GetQuest()
        {
            return quest;
        }

        public IEnumerable<string> GetOutstandingObjectives()
        {
            foreach (var objective in quest.GetObjectives())
            {
                if (!completedObjectives.Contains(objective.reference))
                {
                    yield return objective.description;
                }
            }
        }

        public bool IsComplete()
        {
            return GetCompletedCount() == quest.GetObjectivesNum();
        }
    }
}