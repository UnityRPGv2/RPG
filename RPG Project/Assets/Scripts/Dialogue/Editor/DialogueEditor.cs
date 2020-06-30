using UnityEditor;
using UnityEngine;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue;
        GUIStyle nodeStyle;

        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        [UnityEditor.Callbacks.OnOpenAsset(1)]
        public static bool OpenDialogue(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if (dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false; // we did not handle the open
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;
            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.normal.textColor = Color.white;
            nodeStyle.border = new RectOffset(12, 12, 12, 12);
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
        }

        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            if (newDialogue != null)
            {
                selectedDialogue = newDialogue;
            }
            Repaint();
        }

        // Only called when there is a reason. E.g. GetWindow called.
        private void OnGUI() {
            if (selectedDialogue == null)
            {
                return;
            }
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                OnGUINode(node);
            }
        }

        private void OnGUINode(DialogueNode node)
        {
            GUILayout.BeginArea(node.rect, nodeStyle);
            EditorGUILayout.LabelField(node.uniqueID, EditorStyles.whiteLabel);
            EditorGUI.BeginChangeCheck();
            string newText = EditorGUILayout.TextArea(node.text, EditorStyles.textArea);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selectedDialogue, "Dialogue Text Change");
                node.text = newText;
            }
            GUILayout.EndArea();
        }
    }
}