using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] Dialogue dialogue;
        [SerializeField] DialogueNode currentNode;

        // public bool IsChoosing()
        // {

        // }

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
            if (currentNode == null) return;

            List<DialogueNode> choices = new List<DialogueNode>(dialogue.GetChildren(currentNode));
            int choice = Random.Range(0, choices.Count);
            currentNode = choices[choice];
        }

        public bool HasNext()
        {
            if (currentNode == null) return false;

            List<DialogueNode> choices = new List<DialogueNode>(dialogue.GetChildren(currentNode));
            return choices.Count > 0;
        }

        // public void ChooseNext(int choice)
        // {

        // }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}
