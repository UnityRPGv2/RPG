using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        Dialogue currentDialogue;
        DialogueNode currentNode = null;
        bool isChoosing = false;

        public event Action conversationUpdated;

        public void StartDialogue(Dialogue dialogue)
        {
            currentDialogue = dialogue;
            Next();
        }
        
        public void EndDialogue()
        {
            currentDialogue = null;
            currentNode = null;
            if (conversationUpdated != null)
            {
                conversationUpdated();
            }
        }

        public bool HasActiveConversation()
        {
            return currentDialogue != null;
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public string GetCurrentText()
        {
            if (currentNode == null)
            {
                return "";
            }

            return currentNode.GetText();
        }

        public IEnumerable<DialogueNode> GetCurrentChoices()
        {
            return currentDialogue.GetChildren(currentNode);
        }

        public void Next()
        {
            if (currentDialogue.IsPlayerNext(currentNode))
            {
                isChoosing = true;
                if (conversationUpdated != null)
                {
                    conversationUpdated();
                }
                return;
            }

            List<DialogueNode> choices = new List<DialogueNode>(currentDialogue.GetChildren(currentNode));
            int choice = UnityEngine.Random.Range(0, choices.Count);
            currentNode = choices[choice];
            if (conversationUpdated != null)
            {
                conversationUpdated();
            }
        }

        public bool HasNext()
        {
            if (currentDialogue == null) return false;
            if (isChoosing) return false;

            List<DialogueNode> choices = new List<DialogueNode>(currentDialogue.GetChildren(currentNode));
            return choices.Count > 0;
        }

        public void ChooseNext(DialogueNode choice)
        {
            currentNode = choice;
            isChoosing = false;
            Next();
        }

    }
}
