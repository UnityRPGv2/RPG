using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] Dialogue dialogue;
        DialogueNode currentNode = null;
        bool isChoosing = false;

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

        // public string[] GetCurrentChoices()
        // {

        // }

        public void Next()
        {
            if (dialogue.IsPlayerNext(currentNode))
            {
                isChoosing = true;
                return;
            }

            List<DialogueNode> choices = new List<DialogueNode>(dialogue.GetChildren(currentNode));
            int choice = Random.Range(0, choices.Count);
            currentNode = choices[choice];
        }

        public bool HasNext()
        {
            if (isChoosing) return false;

            List<DialogueNode> choices = new List<DialogueNode>(dialogue.GetChildren(currentNode));
            return choices.Count > 0;
        }

        // public void ChooseNext(int choice)
        // {

        // }

        // Start is called before the first frame update
        void Start()
        {
            Next();
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}
