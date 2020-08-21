using UnityEngine;
using RPG.Core;

namespace RPG.Quests
{
    public class QuestCondition : MonoBehaviour, IConditionEvaluator
    {
        [SerializeField] string condition;
        [SerializeField] bool value;

        public bool Evaluate(string queryCondition)
        {
            if (queryCondition != condition) return false;

            return value;
        }
    }
}