using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
[CustomEditor(typeof(MapSettings), true)]
public class MapSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapSettings obj = (MapSettings) target;
        // SerializedObject serializedObject = new SerializedObject(obj);
        // SerializedProperty list = serializedObject.FindProperty("pointsMap");
        // EditorGUILayout.PropertyField(list, new GUIContent("My List Test"), true);
        if(GUILayout.Button("Build Object"))
        {
            obj.BuildPath();
        }
        if(GUILayout.Button("Delete Object"))
        {
            obj.Reset();
        }
    }
}

// public class ListTestEditor : EditorWindow
// {

//     [MenuItem(itemName: "TestEditorList", menuItem = "Window/TestList")]
//     public static void Init() { 
//         GetWindow<ListTestEditor>("Haha", true);

//     }

//     Editor editor;

//     MapSettings obj = ScriptableObject.CreateInstance<MapSettings>();
//     SerializedObject serializedObject = new SerializedObject(obj);

//     void OnGUI()
//     {
//         if (!editor) { editor = Editor.CreateEditor(this); }
//         if (editor) { editor.OnInspectorGUI(); }
//     }

//     void OnInspectorUpdate() { Repaint(); }
// }
// [CustomEditor(typeof(MapSettings), true)]
// public class ListTestEditorDrawer : Editor
// {

//     public override void OnInspectorGUI()
//     {
//         MapSettings obj = ScriptableObject.CreateInstance<MapSettings>();
//         SerializedObject serializedObject = new SerializedObject(obj);
//         SerializedProperty list = serializedObject.FindProperty("pointsMap");
//         EditorGUILayout.PropertyField(list, new GUIContent("My List Test"), true);
//     }
// }

