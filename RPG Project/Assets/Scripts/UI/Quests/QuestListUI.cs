using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] Quest[] tmpQuests;
        [SerializeField] QuestItemUI itemPrefab;

        private void Start() {
            foreach (Quest quest in tmpQuests)
            {
                var questUI = Instantiate(itemPrefab, transform);
                questUI.Setup(quest);
            }
        }
    }
}