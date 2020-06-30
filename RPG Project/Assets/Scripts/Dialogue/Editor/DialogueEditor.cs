using UnityEditor;
using UnityEngine;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue;

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
                GUILayout.Label("Name:");
                // node.text = EditorGUILayout.TextArea(node.text);
                string newText = EditorGUILayout.TextArea(node.text);
                if (newText != node.text)
                {
                    node.text = newText;
                    // See how can't update file if not marked.
                    EditorUtility.SetDirty(selectedDialogue);
                }
            }
        }
    }
}