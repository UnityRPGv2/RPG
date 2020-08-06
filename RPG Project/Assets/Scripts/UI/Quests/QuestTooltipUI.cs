using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestTooltipUI : MonoBehaviour
{
    [SerializeField] GameObject objectivePrefab;
    [SerializeField] GameObject completeObjectivePrefab;
    [SerializeField] Transform objectiveContainer;
    [SerializeField] TextMeshProUGUI rewardsText;
    [SerializeField] TextMeshProUGUI titleText;

    public void Setup(Quest quest)
    {
        titleText.text = quest.GetTitle();
        foreach (string objective in quest.GetCompletedObjectives())
        {
            var objectiveUI = Instantiate(completeObjectivePrefab, objectiveContainer);
            objectiveUI.GetComponentInChildren<TextMeshProUGUI>().text = objective;
        }
        foreach (string objective in quest.GetOutstandingObjectives())
        {
            var objectiveUI = Instantiate(objectivePrefab, objectiveContainer);
            objectiveUI.GetComponentInChildren<TextMeshProUGUI>().text = objective;
        }
        rewardsText.text = string.Join(", ", quest.GetRewardNames());
    }
}
