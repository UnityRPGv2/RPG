using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Resources;
using RPG.Stats;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected BaseStats baseStats;
    protected Health health;

    protected void Awake()
    {
        baseStats = GetComponent<BaseStats>();
        baseStats.onLevelUp += LevelUp;
        health = GetComponent<Health>();
    }

    protected void Start() 
    {
        health.Init(baseStats.GetStat(Stat.Health));
    }

    public virtual void TakeDamage(GameObject instigator, float damage)
    {
        health.TakeDamage(damage);
    }

    public virtual void AwardXP(float XPReward)
    {
    }

    private void LevelUp()
    {
        health.UpdateMaxHealth(baseStats.GetStat(Stat.Health));
    }
}
