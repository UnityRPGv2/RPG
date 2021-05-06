using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class AbilityData : IAction
{
    GameObject user;
    Vector3 targetedPoint;
    IEnumerable<GameObject> targets;
    bool cancelled = false;

    public Action cancel;

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

    public Vector3 GetTargetedPoint()
    {
        return targetedPoint;
    }

    public void SetTargetedPoint(Vector3 targetedPoint)
    {
        this.targetedPoint = targetedPoint;
    }

    public GameObject GetUser()
    {
        return user;
    }

    public Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return user.GetComponent<MonoBehaviour>().StartCoroutine(coroutine);
    }

    public void Cancel()
    {
        cancelled = true;
        if (cancel != null) cancel();
    }

    public bool GetCancelled()
    {
        return cancelled;
    }
}