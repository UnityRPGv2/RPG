using System;
using GameDevTV.Inventories;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "RPG/Quest", order = 0)]
public class Quest : ScriptableObject {
    [SerializeField] string[] objectives;
    [SerializeField] InventoryItem[] rewards;

    public string GetTitle()
    {
        return name;
    }

    public int GetObjectivesNum()
    {
        return objectives.Length;
    }
}