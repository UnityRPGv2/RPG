using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.Dialogue
{
    [CreateAssetMenu(menuName = ("RPG/Dialogue"))]
    public class Dialogue : ScriptableObject
    {
        [SerializeField]
        List<DialogueNode> nodes = new List<DialogueNode>();
        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

#if UNITY_EDITOR
        private void Awake() {
            if (nodes.Count <= 0)
            {
                nodes.Add(new DialogueNode());
            }
        }
#endif

        private void OnValidate() {
            nodeLookup.Clear();
            foreach (DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.uniqueID] = node;
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public IEnumerable<DialogueNode> GetChildren(DialogueNode node)
        {
            foreach (string childID in node.children)
            {
                if (nodeLookup.ContainsKey(childID))
                {
                    yield return nodeLookup[childID];
                }
            }
        }

        public DialogueNode CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = new DialogueNode();
            newNode.uniqueID = System.Guid.NewGuid().ToString();
            Vector2 childOffset = new Vector2(200, 0);
            newNode.rect.position = parent.rect.position + childOffset;
            parent.children.Add(newNode.uniqueID);
            nodes.Add(newNode);
            OnValidate();
            return newNode;
        }

        public void DeleteNode(DialogueNode deletingNode)
        {
            nodes.Remove(deletingNode);
            OnValidate();
            CleanDanglingChildren(deletingNode.uniqueID);
        }

        private void CleanDanglingChildren(string IDToRemove)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.children.Remove(IDToRemove);
            }
        }
    }
}
