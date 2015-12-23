using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/*
 * Author : Cryptoz
 * Version : 1.0
*/

public class EbsPrefabEditorWindow : EditorWindow
{
    private GameObject prefabBuild;
    private GameObject prefabPreview;
    private bool canRotate;
    private float angleRotate;
    private LayerMask layerMask;
    private BuildType buildEnum;

    private void OnGUI()
    {
        GUILayout.BeginVertical("box", GUILayout.Width(255));

        GUILayout.BeginHorizontal("box");

        GUILayout.Label("Prefab : ");

        prefabBuild = (GameObject)EditorGUILayout.ObjectField(prefabBuild, typeof(GameObject), true);

        GUILayout.EndHorizontal();

        if (prefabBuild != null)
        {
            GUILayout.BeginHorizontal("box");

            GUILayout.Label("Prefab Name : ");

            prefabBuild.name = EditorGUILayout.TextField(prefabBuild.name);

            GUILayout.EndHorizontal();
        }

        GUILayout.BeginHorizontal("box");

        GUILayout.Label("Prefab Preview : ");

        prefabPreview = (GameObject)EditorGUILayout.ObjectField(prefabPreview, typeof(GameObject), true);

        GUILayout.EndHorizontal();

        if (prefabPreview != null)
        {
            GUILayout.BeginHorizontal("box");

            GUILayout.Label("Prefab Name : ");

            prefabPreview.name = EditorGUILayout.TextField(prefabPreview.name);

            GUILayout.EndHorizontal();
        }

        GUILayout.BeginHorizontal("box");

        GUILayout.Label("Can Rotate : ");

        canRotate = EditorGUILayout.Toggle(canRotate);

        GUILayout.EndHorizontal();

        if (canRotate)
        {
            GUILayout.BeginHorizontal("box");

            GUILayout.Label("Angle Rotate : ");

            angleRotate = EditorGUILayout.FloatField(angleRotate);

            GUILayout.EndHorizontal();
        }

        GUILayout.BeginHorizontal("box");

        GUILayout.Label("Layer Mask : ");

        layerMask = LayerMaskField("", layerMask);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box");

        GUILayout.Label("Build Type : ");

        buildEnum = (BuildType)EditorGUILayout.EnumPopup(buildEnum);

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create"))
        {
            GameObject TempBuild = prefabBuild;
            GameObject TempPreview = prefabPreview;

            string buildName = TempBuild.name;
            string previewName = TempPreview.name;

            //Root for build
            GameObject rootBuild = new GameObject(buildName);
            rootBuild.transform.position = TempBuild.transform.position;
            TempBuild.transform.parent = rootBuild.transform;
            Build buildCom = rootBuild.AddComponent<Build>();
            buildCom.CanRotate = canRotate;
            buildCom.DegressRotate = angleRotate;
            buildCom.Layer = layerMask;
            buildCom.Type = buildEnum;

            //Root for preview
            GameObject rootPreview = new GameObject(previewName + " (Preview)");
            rootPreview.transform.position = TempPreview.transform.position;
            TempPreview.transform.parent = rootPreview.transform;

            Object AssetPrefabBuild = PrefabUtility.CreateEmptyPrefab("Assets/EasyBuildSystem/EBS/Prefabs/" + buildName + ".prefab");
			Object AssetPrefabPreview = PrefabUtility.CreateEmptyPrefab("Assets/EasyBuildSystem/EBS/Prefabs/" + previewName + " (Preview)" + ".prefab");

            GameObject finishBuild = PrefabUtility.ReplacePrefab(rootBuild, AssetPrefabBuild);
            GameObject finishPreview = PrefabUtility.ReplacePrefab(rootPreview, AssetPrefabPreview);

            rootBuild.GetComponent<Build>().Prefab = finishBuild;
            rootBuild.GetComponent<Build>().PrefabPreview = finishPreview;

            finishBuild.GetComponent<Build>().Prefab = finishBuild;
            finishBuild.GetComponent<Build>().PrefabPreview = finishPreview;

            AssetDatabase.Refresh();

#if UNITY_EDITOR
            UnityEditor.SceneView.lastActiveSceneView.FrameSelected();
#endif

            Debug.Log("New prefab create (" + buildName + ").");

            base.Close();
        }

        if (GUILayout.Button("Cancel"))
            base.Close();

        GUILayout.FlexibleSpace();

        GUILayout.EndVertical();
    }

    public static LayerMask LayerMaskField(string label, LayerMask selected)
    {
        return LayerMaskField(label, selected, true);
    }

    public static LayerMask LayerMaskField(string label, LayerMask selected, bool showSpecial)
    {
        List<string> layers = new List<string>();
        List<int> layerNumbers = new List<int>();

        string selectedLayers = "";

        for (int i = 0; i < 32; i++)
        {
            string layerName = LayerMask.LayerToName(i);

            if (layerName != "")
            {
                if (selected == (selected | (1 << i)))
                {
                    if (selectedLayers == "")
                    {
                        selectedLayers = layerName;
                    }
                    else
                    {
                        selectedLayers = "Mixed";
                    }
                }
            }
        }

        if (Event.current.type != EventType.MouseDown && Event.current.type != EventType.ExecuteCommand)
        {
            if (selected.value == 0)
            {
                layers.Add("Nothing");
            }
            else if (selected.value == -1)
            {
                layers.Add("Everything");
            }
            else
            {
                layers.Add(selectedLayers);
            }
            layerNumbers.Add(-1);
        }

        if (showSpecial)
        {
            layers.Add((selected.value == 0 ? "[X] " : "     ") + "Nothing");
            layerNumbers.Add(-2);

            layers.Add((selected.value == -1 ? "[X] " : "     ") + "Everything");
            layerNumbers.Add(-3);
        }

        for (int i = 0; i < 32; i++)
        {
            string layerName = LayerMask.LayerToName(i);

            if (layerName != "")
            {
                if (selected == (selected | (1 << i)))
                {
                    layers.Add("[X] " + layerName);
                }
                else
                {
                    layers.Add("     " + layerName);
                }
                layerNumbers.Add(i);
            }
        }

        bool preChange = GUI.changed;

        GUI.changed = false;

        int newSelected = 0;

        if (Event.current.type == EventType.MouseDown)
        {
            newSelected = -1;
        }

        newSelected = EditorGUILayout.Popup(label, newSelected, layers.ToArray());

        if (GUI.changed && newSelected >= 0)
        {
            if (showSpecial && newSelected == 0)
            {
                selected = 0;
            }
            else if (showSpecial && newSelected == 1)
            {
                selected = -1;
            }
            else
            {
                if (selected == (selected | (1 << layerNumbers[newSelected])))
                {
                    selected &= ~(1 << layerNumbers[newSelected]);
                }
                else
                {
                    selected = selected | (1 << layerNumbers[newSelected]);
                }
            }
        }
        else
        {
            GUI.changed = preChange;
        }

        return selected;
    }
}