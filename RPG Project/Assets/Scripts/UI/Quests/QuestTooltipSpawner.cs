using System.Collections;
using System.Collections.Generic;
using GameDevTV.Core.UI.Tooltips;
using UnityEngine;
using RPG.Quests;

namespace RPG.UI.Quests
{
    public class QuestTooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            var questTooltip = tooltip.GetComponent<QuestTooltipUI>();
            questTooltip.Setup(GetComponent<QuestItemUI>().GetQuest());
        }
    }
}