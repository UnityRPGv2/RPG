using UnityEngine;

namespace RPG.Quests
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] Quest quest;
        QuestList playerQuestList;

        private void Start() {
            playerQuestList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        }

        public void GiveQuest()
        {
            playerQuestList.AddQuest(quest);
        }
    }
}