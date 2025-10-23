using Builder;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StructureBuilder))]
public class StructureBuilderEditor : Editor
{
    // The height of the text area in the Inspector
    private const int TEXT_AREA_HEIGHT = 150; 

    public override void OnInspectorGUI()
    {
        // 1. Get the reference to the component we are editing
        StructureBuilder builder = (StructureBuilder)target;

        // 2. Draw the Block Database field first.
        // We need to use SerializedProperty to properly handle ScriptableObject references.
        SerializedProperty dbProperty = serializedObject.FindProperty("blockDB");
        EditorGUILayout.PropertyField(dbProperty, new GUIContent("Block Database"));
        
        // Apply any changes made to the database reference
        serializedObject.ApplyModifiedProperties();
        
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("--- Blueprint Text Input ---", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Format: X, Y, Z, BlockType\n(e.g., 0,0,0,dirt)", MessageType.Info);
        
        // 3. Draw the large multi-line text area
        // We directly access and modify the public string field 'blueprintText'
        builder.blueprintText = EditorGUILayout.TextArea(
            builder.blueprintText, 
            GUILayout.Height(TEXT_AREA_HEIGHT)
        );

        // 4. Draw the build button
        if (GUILayout.Button("PARSED BUILD STRUCTURE"))
        {
            // Call the BuildStructure method on the target component
            builder.BuildStructure();
        }

        // 5. Tell Unity that changes have been made to the target object (the Monobehaviour).
        // This is necessary to ensure the 'blueprintText' string is saved to the scene/prefab.
        if (GUI.changed)
        {
            EditorUtility.SetDirty(builder);
        }
    }
}