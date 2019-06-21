using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Resources;
using RPG.Stats;
using UnityEngine;

public class Character : MonoBehaviour
{
    BaseStats baseStats;
    Health health;
    Experience experience;
    Fighter fighter;

    void Awake()
    {
        baseStats = GetComponent<BaseStats>();
        baseStats.onLevelUp += LevelUp;
        health = GetComponent<Health>();
        fighter = GetComponent<Fighter>();
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
        // Not yet initing all dependencies of fighter.
        fighter.damage = baseStats.GetStat(Stat.Damage);
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

    public int level => baseStats.GetLevel();

    private void UpdateBaseStatXP()
    {
        baseStats.UpdateLevel(experience.GetPoints());
    }

    private void LevelUp()
    {
        health.UpdateMaxHealth(baseStats.GetStat(Stat.Health));
        fighter.damage = baseStats.GetStat(Stat.Damage);
    }
}
