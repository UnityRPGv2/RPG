using UnityEngine;

namespace RPG.Quests
{
    public class QuestCompletion : MonoBehaviour
    {
        [SerializeField] Quest quest;
        [SerializeField] string objective;
        QuestList playerQuestList;

        private void Start()
        {
            playerQuestList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        }

        public void CompleteObjective()
        {
            playerQuestList.CompleteObjective(quest, objective);
        }

    }
}