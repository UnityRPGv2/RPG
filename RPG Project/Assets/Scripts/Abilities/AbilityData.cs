using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData
{
    GameObject user;
    IEnumerable<GameObject> targets;

    public AbilityData(GameObject user)
    {
        this.user = user;
    }

    public IEnumerable<GameObject> GetTargets()
    {
        return targets;
    }

    public void SetTargets(IEnumerable<GameObject> targets)
    {
        this.targets = targets; 
    }

    public GameObject GetUser()
    {
        return user;
    }
}