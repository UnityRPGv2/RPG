using UnityEngine;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour
    {
        [SerializeField] float maxMana = 200;
        [SerializeField] float manaRegenRate = 2;

        float mana;

        private void Awake() {
            mana = maxMana;
        }

        private void Update() {
            if (mana < maxMana)
            {
                mana += manaRegenRate * Time.deltaTime;
                if (mana > maxMana)
                {
                    mana = maxMana;
                }
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

        public bool UseMana(float manaToUse)
        {
            if (manaToUse > mana)
            {
                return false;
            }
            mana -= manaToUse;
            return true;
        }
    }
}