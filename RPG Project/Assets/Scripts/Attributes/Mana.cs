using System;
using GameDevTV.Utils;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour
    {
        [SerializeField] float maxMana = 100;
        float mana;

        private void Awake() {
            mana = maxMana;
        }

        public float GetMana()
        {
            return mana;
        }

        public float GetMaxMana()
        {
            return maxMana;
        }

        public bool UseMana(float amount)
        {
            if (mana < amount)
            {
                return false;
            }
            mana -= amount;
            return true;
        }
    }
}