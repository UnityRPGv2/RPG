using UnityEngine;
using RPG.Core;

namespace RPG.Quests
{
    public class QuestCondition : MonoBehaviour, IConditionEvaluator
    {
        [SerializeField] Quest quest;
        [SerializeField] string objective;
        [SerializeField] string conditionName;
        QuestList playerQuestList;

        private void Start()
        {
            playerQuestList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        }

        public bool Evaluate(string condition)
        {
            if (condition != conditionName) return false;
            if (string.IsNullOrEmpty(objective))
            {
                return playerQuestList.HasQuest(quest);
            }
            return playerQuestList.IsComplete(quest, objective);
        }
    }
}