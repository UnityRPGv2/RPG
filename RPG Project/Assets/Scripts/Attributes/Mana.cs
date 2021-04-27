using System;
using GameDevTV.Saving;
using GameDevTV.Utils;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour, ISaveable
    {
        LazyValue<float> mana;

        private void Awake() {
            mana = new LazyValue<float>(GetMaxMana);
        }

        private void Update() {
            if (mana.value < GetMaxMana())
            {
                RegenerateMana(GetRegenerationRate() * Time.deltaTime);
            }
        }

        public void RegenerateMana(float points)
        {
            mana.value += points;
            float maxMana = GetMaxMana();
            if (mana.value > maxMana)
            {
                mana.value = maxMana;
            }
        }

        public float GetMana()
        {
            return mana.value;
        }

        public float GetMaxMana()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Mana);
        }

        public bool UseMana(float amount)
        {
            if (mana.value < amount)
            {
                return false;
            }
            mana.value -= amount;
            return true;
        }

        private float GetRegenerationRate()
        {
            return GetComponent<BaseStats>().GetStat(Stat.ManaRegeneration);
        }

        public object CaptureState()
        {
            return mana.value;
        }

        public void RestoreState(object state)
        {
            mana.value = (float) state;
        }
    }
}