using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;


        void Update()
        {
            foreground.localScale = new Vector3(healthComponent.GetFraction(), 1, 1);
        }
    }
}