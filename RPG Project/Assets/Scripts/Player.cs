using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;

public class Player : Character
{
    Experience experience;

    new void Awake() {
        print("Player awake");
        base.Awake();
        experience = GetComponent<Experience>();
        if (experience != null)
            experience.onExperienceGained += UpdateBaseStatXP;
    }

    new void Start()
    {
        baseStats.SetStartingXP(experience.GetPoints());
        base.Start();
    }

    private void UpdateBaseStatXP()
    {
        baseStats.UpdateLevel(experience.GetPoints());
    }

    public override void AwardXP(float XPReward)
    {
        experience.GainExperience(XPReward);
    }
}
