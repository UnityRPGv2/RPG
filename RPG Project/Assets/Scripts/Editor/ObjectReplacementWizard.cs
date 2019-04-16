/* This wizard will replace a selection with an object or prefab.
 * Scene objects will be cloned (destroying their prefab links).
 * Original coding by 'yesfish', nabbed from Unity Forums
 * 'keep parent' added by Dave A (also removed 'rotation' option, using localRotation
 */
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ObjectReplacementWizard : ScriptableWizard
{
    public bool copyValues = true;
    public GameObject NewType;
    public GameObject[] OldObjects;

    [MenuItem("Custom/Replace GameObjects")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<ObjectReplacementWizard>("Replace GameObjects", "Replace");
    }

    void OnWizardCreate()
    {
        //Transform[] Replaces;
        //Replaces = Replace.GetComponentsInChildren<Transform>();

        foreach (GameObject go in OldObjects)
        {
            GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(NewType);
            Undo.RegisterCreatedObjectUndo(newObject, "Created prefab");
            newObject.name = go.name;
            newObject.transform.parent = go.transform.parent;
            
            var monoBehaviours = go.GetComponents<MonoBehaviour>();
            var components = go.GetComponents<Component>();
            var allComponents = new Object[components.Length + monoBehaviours.Length];
            monoBehaviours.CopyTo(allComponents, 0);
            components.CopyTo(allComponents, monoBehaviours.Length);
            foreach (var component in allComponents)
            {
                var so = new SerializedObject(component);
                var prop = so.GetIterator();
                do
                {
                    if (prop.prefabOverride)
                    {
                        var otherSo = new SerializedObject(newObject.GetComponent(component.GetType()));
                        otherSo.CopyFromSerializedProperty(prop);
                        otherSo.ApplyModifiedProperties();
                    }
                }
                while (prop.NextVisible(true));
            }

            Undo.DestroyObjectImmediate(go);
        }

    }
}