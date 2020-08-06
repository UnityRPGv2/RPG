using UnityEngine;
using RPG.Quests;

namespace RPG.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] QuestItemUI itemPrefab;
        [SerializeField] QuestItemUI completedPrefab;
        QuestList questList;

        private void Start() {
            questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questList.onUpdate += UpdateList;
            UpdateList();
        }

        private void UpdateList() {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
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