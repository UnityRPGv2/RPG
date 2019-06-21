using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        Character character;

        private void Awake()
        {
            character = GameObject.FindWithTag("Player").GetComponent<Character>();
        }

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}", character.level);
        }
    }
}