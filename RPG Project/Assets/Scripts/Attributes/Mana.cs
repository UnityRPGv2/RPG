using System;
using GameDevTV.Utils;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour
    {
        float mana = 100;

        public float GetMana()
        {
            return mana;
        }

        public bool UseMana(float amount)
        {
            if (mana < amount)
            {
                return false;
            }
            mana -= amount;
            Debug.Log($"Mana {mana}");
            return true;
        }
    }
}