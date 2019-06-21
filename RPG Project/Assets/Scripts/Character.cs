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
        health = GetComponent<Health>();
        experience = GetComponent<Experience>();
    }

    private void Start() 
    {
        if (experience)
        {
            baseStats.Init(experience);
        }
        health.Init(baseStats);
    }
}
