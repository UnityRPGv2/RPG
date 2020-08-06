using UnityEngine;
using RPG.Quests;

namespace RPG.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] QuestItemUI itemPrefab;

        private void Start() {
            var questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            foreach (QuestStatus quest in questList.GetQuestStatuses())
            {
                var questUI = Instantiate(itemPrefab, transform);
                questUI.Setup(quest);
            }
        }
    }
}