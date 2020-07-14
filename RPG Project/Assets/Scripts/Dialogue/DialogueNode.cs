using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] string text;
        [SerializeField] Rect rect = new Rect(40, 40, 200, 100);
        [SerializeField] List<string> children = new List<string>();
        
        public string GetText()
        {
            return text;
        }

        public IEnumerable<string> GetChildren()
        {
            return children;
        }

        public bool HasChild(string childID)
        {
            return children.Contains(childID);
        }

        public Rect GetRect()
        {
            return rect;
        }

#if UNITY_EDITOR
        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Change Dialogue Node Text");
                text = newText;
            }
        }

        public void AddChild(string childID)
        {
            if (!children.Contains(childID))
            {
                Undo.RecordObject(this, "Link Dialogue Node");
                children.Add(childID);
            }
        }

        public void RemoveChild(string childID)
        {
            if (children.Contains(childID))
            {
                Undo.RecordObject(this, "Unlink Dialogue Node");
                children.Remove(childID);
            }
        }

        public void SetPosition(Vector2 newPos)
        {
            if (newPos != rect.position)
            {
                Undo.RecordObject(this, "Move Dialogue Node");
                rect.position = newPos;
            }
        }
#endif
    }
}