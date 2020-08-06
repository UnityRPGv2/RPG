using System;
using TMPro;
using UnityEngine;
using RPG.Quests;

namespace RPG.UI.Quests
{
    public class QuestItemUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] TextMeshProUGUI completion;
        QuestStatus questStatus;

        public void Setup(QuestStatus status)
        {
            text.text = status.GetQuest().GetTitle();
            completion.text =  status.GetCompletedCount() + "/" + status.GetQuest().GetObjectivesNum();
            this.questStatus = status;
        }

        public QuestStatus GetQuest()
        {
            return questStatus;
        }
    }
}