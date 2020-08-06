using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    [System.Serializable]
    public class QuestStatus
    {
        [SerializeField] Quest quest;
        [SerializeField] List<string> completedObjectives;

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
    }
}