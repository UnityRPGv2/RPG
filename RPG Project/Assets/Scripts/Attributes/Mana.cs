using System;
using GameDevTV.Utils;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour
    {
        [SerializeField] float maxMana = 100;
        [SerializeField] float regenerationRate = 5;
        float mana;

        private void Awake() {
            mana = maxMana;
        }

        private void Update() {
            if (mana < maxMana)
            {
                RegenerateMana(regenerationRate * Time.deltaTime);
            }
        }

        public void RegenerateMana(float points)
        {
            mana += points;
            if (mana > maxMana)
            {
                mana = maxMana;
            }
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