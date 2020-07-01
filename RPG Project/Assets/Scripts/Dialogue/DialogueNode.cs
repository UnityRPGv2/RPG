using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        public string text;
        public Rect rect = new Rect(40, 40, 200, 100);
        public List<string> children = new List<string>();
    }
}