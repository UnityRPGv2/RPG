using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class SavingWrapper : MonoBehaviour
{
    const string defaultSaveFile = "save";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    private void Load()
    {
        GetComponent<SavingSystem>().Load(defaultSaveFile);
    }

    private void Save()
    {
        GetComponent<SavingSystem>().Save(defaultSaveFile);
    }
}
