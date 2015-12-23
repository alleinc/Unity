using UnityEngine;
using UnityEditor;

/*
 * Author : Cryptoz
 * Version : 1.0
*/

public class EbsEditorWindow : EditorWindow
{
    private GameObject Prefab;

    [MenuItem("Window/EBS Editor")]
    private static void Init()
    {
        EbsEditorWindow window = (EbsEditorWindow)EditorWindow.GetWindow(typeof(EbsEditorWindow));
        window.title = "EBS - Editor";
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical("box");

        Texture2D head = Resources.Load("logo") as Texture2D;
        GUILayout.Box(head, GUILayout.Width(255), GUILayout.Height(95));

        GUILayout.BeginHorizontal("box");

        EditorGUILayout.LabelField("Easy Build System - Version 1.0", EditorStyles.boldLabel);

        GUILayout.EndHorizontal();

        GUILayout.BeginVertical("box", GUILayout.Width(255));

        if (GUILayout.Button("Create Build Prefab"))
        {
            EbsPrefabEditorWindow window = (EbsPrefabEditorWindow)EditorWindow.GetWindow(typeof(EbsPrefabEditorWindow));
            window.title = "EBS - Prefab";
            window.Show();
        }

        if (GUILayout.Button("Create Build Attach"))
        {
            EbsAttachEditorWindow window = (EbsAttachEditorWindow)EditorWindow.GetWindow(typeof(EbsAttachEditorWindow));
            window.title = "EBS - Attach";
            window.Show();
        }

        if (GUILayout.Button("Create Build Area"))
        {
            EbsAreaEditorWindow window = (EbsAreaEditorWindow)EditorWindow.GetWindow(typeof(EbsAreaEditorWindow));
            window.title = "EBS - Area";
            window.Show();
        }

        if (GUILayout.Button("Close"))
            base.Close();

        GUILayout.FlexibleSpace();

        GUILayout.EndVertical();

        GUILayout.EndVertical();
    }
}