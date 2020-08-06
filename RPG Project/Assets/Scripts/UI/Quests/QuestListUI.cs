using UnityEngine;
using RPG.Quests;

namespace RPG.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] QuestItemUI itemPrefab;
        [SerializeField] QuestItemUI completedPrefab;

        private void Start() {
            var questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            foreach (QuestStatus quest in questList.GetQuestStatuses())
            {
                if (quest.IsComplete()) continue;
                var questUI = Instantiate(itemPrefab, transform);
                questUI.Setup(quest);
            }
            foreach (QuestStatus quest in questList.GetQuestStatuses())
            {
                if (!quest.IsComplete()) continue;
                var questUI = Instantiate(completedPrefab, transform);
                questUI.Setup(quest);
            }
        }
    }
}