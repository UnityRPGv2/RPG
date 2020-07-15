using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace RPG.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] bool isPlayerNextSpeaker = true;
        [SerializeField] string text;
        [SerializeField] Rect rect = new Rect(40, 40, 200, 100);
        [SerializeField] List<string> children = new List<string>();
        public event Action OnChange;

        public bool IsPlayerNextSpeaker()
        {
            return isPlayerNextSpeaker;
        }

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

        private void OnValidate() {
            if (OnChange != null)
            {
                OnChange();
            }
        }

#if UNITY_EDITOR
        public void SetNextSpeaker(bool newIsPlayerNextSpeaker)
        {
            if (isPlayerNextSpeaker != newIsPlayerNextSpeaker)
            {
                Undo.RecordObject(this, "Change Dialogue Node Speaker");
                isPlayerNextSpeaker = newIsPlayerNextSpeaker;
                OnValidate();
            }
        }

        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Change Dialogue Node Text");
                text = newText;
                EditorUtility.SetDirty(this);
                OnValidate();
            }
        }

        public void AddChild(string childID)
        {
            if (!children.Contains(childID))
            {
                Undo.RecordObject(this, "Link Dialogue Node");
                children.Add(childID);
                EditorUtility.SetDirty(this);
                OnValidate();
            }
        }

        public void RemoveChild(string childID)
        {
            if (children.Contains(childID))
            {
                Undo.RecordObject(this, "Unlink Dialogue Node");
                children.Remove(childID);
                EditorUtility.SetDirty(this);
                OnValidate();
            }
        }

        public void SetPosition(Vector2 newPos)
        {
            if (newPos != rect.position)
            {
                Undo.RecordObject(this, "Move Dialogue Node");
                rect.position = newPos;
                EditorUtility.SetDirty(this);
                OnValidate();
            }
        }
#endif
    }
}