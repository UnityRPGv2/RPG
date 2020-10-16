using System.Collections;
using System.Collections.Generic;
using GameDevTV.Core.UI.Tooltips;
using UnityEngine;

public class QuestTooltipSpawner : TooltipSpawner
{
    public override bool CanCreateTooltip()
    {
        return true;
    }

    public override void UpdateTooltip(GameObject tooltip)
    {
    }
}
