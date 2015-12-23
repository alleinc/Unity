using UnityEngine;
using UnityEditor;

/*
 * Author : Cryptoz
 * Version : 1.0
*/

public class EbsAttachEditorWindow : EditorWindow
{
    private string prefabName;
    private BuildType buildType;

    private void OnGUI()
    {
        GUILayout.BeginVertical("box", GUILayout.Width(255));

        GUILayout.BeginHorizontal("box");

        GUILayout.Label("Prefab Name : ");

        prefabName = EditorGUILayout.TextField(prefabName);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box");

        GUILayout.Label("Build Type : ");

        buildType = (BuildType)EditorGUILayout.EnumPopup(buildType);

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create"))
        {
            GameObject go = new GameObject(prefabName);
            Selection.activeGameObject = go;

            BuildAttach temp = go.AddComponent<BuildAttach>();
            temp.Type = buildType;

#if UNITY_EDITOR
            UnityEditor.SceneView.lastActiveSceneView.FrameSelected();
#endif

            Debug.Log("New attach create (" + prefabName + ").");

            base.Close();
        }

        if (GUILayout.Button("Close"))
            base.Close();

        GUILayout.FlexibleSpace();

        GUILayout.EndVertical();
    }
}