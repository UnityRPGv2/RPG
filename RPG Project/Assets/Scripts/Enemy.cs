using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;

public class Enemy : Character
{
    public override void TakeDamage(GameObject instigator, float damage)
    {
        base.TakeDamage(instigator, damage);
        // This would be passed in as character to avoid this.
        Character character = instigator.GetComponent<Character>();
        if (health.IsDead())
        {
            character.AwardXP(baseStats.GetStat(Stat.ExperienceReward));
        }
    }
}
