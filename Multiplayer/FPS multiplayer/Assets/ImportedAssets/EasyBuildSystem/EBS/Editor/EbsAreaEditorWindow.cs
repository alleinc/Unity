using UnityEngine;
using UnityEditor;

/*
 * Author : Cryptoz
 * Version : 1.0
*/

public class EbsAreaEditorWindow : EditorWindow
{
    private string prefabName;
    private bool canBuild;

    private void OnGUI()
    {
        GUILayout.BeginVertical("box", GUILayout.Width(255));

        GUILayout.BeginHorizontal("box");

        GUILayout.Label("Prefab Name : ");

        prefabName = EditorGUILayout.TextField(prefabName);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box");

        GUILayout.Label("Can Build : ");

        canBuild = EditorGUILayout.Toggle(canBuild);

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create"))
        {
            GameObject go = new GameObject(prefabName);
            Selection.activeGameObject = go;

            BuildArea temp = go.AddComponent<BuildArea>();
            temp.CanBuild = canBuild;

#if UNITY_EDITOR
            UnityEditor.SceneView.lastActiveSceneView.FrameSelected();
#endif

            Debug.Log("The area create (" + prefabName + ").");
        }

        if (GUILayout.Button("Close"))
            base.Close();

        GUILayout.FlexibleSpace();

        GUILayout.EndVertical();
    }
}