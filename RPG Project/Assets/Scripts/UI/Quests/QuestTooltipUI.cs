using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using RPG.Quests;

public class QuestTooltipUI : MonoBehaviour
{
    [SerializeField] GameObject objectivePrefab;
    [SerializeField] GameObject completeObjectivePrefab;
    [SerializeField] Transform objectiveContainer;
    [SerializeField] TextMeshProUGUI rewardsText;
    [SerializeField] TextMeshProUGUI titleText;

    public void Setup(QuestStatus status)
    {
        titleText.text = status.GetQuest().GetTitle();
        foreach (string objective in status.GetCompletedObjectives())
        {
            var objectiveUI = Instantiate(completeObjectivePrefab, objectiveContainer);
            objectiveUI.GetComponentInChildren<TextMeshProUGUI>().text = objective;
        }
        foreach (string objective in status.GetOutstandingObjectives())
        {
            var objectiveUI = Instantiate(objectivePrefab, objectiveContainer);
            objectiveUI.GetComponentInChildren<TextMeshProUGUI>().text = objective;
        }
        rewardsText.text = string.Join(", ", status.GetQuest().GetRewardNames());
    }
}
