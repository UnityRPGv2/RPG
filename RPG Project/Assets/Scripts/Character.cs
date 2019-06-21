using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Resources;
using RPG.Stats;
using UnityEngine;

public class Character : MonoBehaviour
{
    BaseStats baseStats;
    Health health;
    Experience experience;

    void Awake()
    {
        baseStats = GetComponent<BaseStats>();
        baseStats.onLevelUp += LevelUp;
        health = GetComponent<Health>();
        experience = GetComponent<Experience>();
        if (experience != null)
            experience.onExperienceGained += UpdateBaseStatXP;
    }

    private void Start() 
    {
        if (experience)
        {
            baseStats.SetStartingXP(experience.GetPoints());
        }
        health.Init(baseStats.GetStat(Stat.Health));
    }

    public void TakeDamage(GameObject instigator, float damage)
    {
        health.TakeDamage(damage);
        Character character = instigator.GetComponent<Character>();
        if (health.IsDead() && character != null && character.hasXP)
        {
            character.AwardXP(baseStats.GetStat(Stat.ExperienceReward));
        }
    }

    public bool hasXP => experience != null;

    public void AwardXP(float XPReward)
    {
        experience.GainExperience(XPReward);
    }

    private void UpdateBaseStatXP()
    {
        baseStats.UpdateLevel(experience.GetPoints());
    }

    private void LevelUp()
    {
        health.UpdateMaxHealth(baseStats.GetStat(Stat.Health));
    }
}
