using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.FloatingText
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] Text damageText = null;
        [SerializeField] string format = "{0:0}";

        public void DestroyText()
        {
            Destroy(gameObject);
        }

        public void SetValue(float amount)
        {
            damageText.text = String.Format(format, amount);
        }
    }
}