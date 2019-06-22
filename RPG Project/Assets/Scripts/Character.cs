using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Resources;
using RPG.Stats;
using UnityEngine;

public class Character : MonoBehaviour
{
    private void Start() 
    {
        var experience = GetComponent<Experience>();
        var mover = GetComponent<Mover>();
        var animator = GetComponent<Animator>();
        var actionScheduler = GetComponent<ActionScheduler>();

        var baseStats = GetComponent<BaseStats>();
        baseStats.Init(experience);

        var health = GetComponent<Health>();
        health.Init(baseStats);

        var fighter = GetComponent<Fighter>();
        fighter.Init(mover, animator, actionScheduler, baseStats);
    }
}
