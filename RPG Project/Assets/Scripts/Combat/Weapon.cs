using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Weapon : MonoBehaviour
    {
        public void OnHit()
        {
            print("Weapon Hit " + gameObject.name);
        }
    }
}