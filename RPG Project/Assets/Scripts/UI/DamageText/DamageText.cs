using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        public void DestroyText()
        {
            Destroy(gameObject);
        }
    }
}