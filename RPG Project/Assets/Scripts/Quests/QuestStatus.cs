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

        [System.Serializable]
        struct QuestStatusRecord
        {
            public string questName;
            public List<string> completedObjectives;
        }

        public QuestStatus(object state)
        {
            var record = (QuestStatusRecord) state;
            quest = Quest.GetByName(record.questName);
            completedObjectives = record.completedObjectives;
        }

        public object CaptureState()
        {
            var record = new QuestStatusRecord();
            record.questName = quest.name;
            record.completedObjectives = completedObjectives;
            return record;
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

        public void CompleteObjective(string objective)
        {
            if (!quest.HasObjective(objective)) return;
            if (completedObjectives.Contains(objective)) return;
            completedObjectives.Add(objective);
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