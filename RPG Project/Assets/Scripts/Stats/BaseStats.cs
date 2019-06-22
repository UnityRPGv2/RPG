using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Utils;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifiers = false;

        public event Action onLevelUp;

        LazyValue<int> currentLevel;

        Experience experience;

        private void Awake() {
            experience = GetComponent<Experience>();
            currentLevel = new LazyValue<int>(CalculateLevel);
        }

        private void Start() 
        {
            currentLevel.ForceInit();
        }

        private void OnEnable() {
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void OnDisable() {
            if (experience != null)
            {
                experience.onExperienceGained -= UpdateLevel;
            }
        }

        private void UpdateLevel() 
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + GetPercentageModifier(stat)/100);
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            return currentLevel.value;
        }

        private float GetAdditiveModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetPercentageModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;

            float currentXP = experience.GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }
    }
}