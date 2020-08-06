using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestList : MonoBehaviour
    {
        [SerializeField] List<QuestStatus> tmpQuests;

        public IEnumerable<QuestStatus> GetQuestStatuses()
        {
            return tmpQuests;
        }
    }
}