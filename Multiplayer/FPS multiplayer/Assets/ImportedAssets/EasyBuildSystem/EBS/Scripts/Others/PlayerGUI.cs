using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    public BuildMode Build;

	private void Awake()
	{
		if (Build == null)
			Build = GetComponent<BuildMode>();
	}

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Screen.lockCursor = false;
            Cursor.visible = true;
        }

        if (Input.GetKey(KeyCode.B))
        {
            Screen.lockCursor = true;
            Cursor.visible = false;
        }
    }

    private void OnGUI()
    {
		if (!Screen.lockCursor)
			GUI.Box(new Rect(Screen.width/2-150, 15, 300, 23), "<color=yellow><b>Press the key <i>B</i> for the lock the cursor.</b></color>");

		if (Build == null)
			return;

		GUI.Label(new Rect(10, Screen.height - 110, 350, 100), "<b>Controls : </b>\n" +
		        "<color=cyan>Select Prefab : <b>Mouse Wheel Up/Down" + "</b>\n" +
		        "Active Build Mode : <b>" + Build.KeyForBuild + "</b>\n" +
		        "Active Destroy Mode : <b>" + Build.KeyForDestroy + "</b>\n" +
		        "Cancel Current Mode : <b>Right Click</b>" + "\n" +
		        "Build Current Prefab : <b>Left Click</b>" + "</color>");

        GUILayout.BeginArea(new Rect(10, 10, 150, 400), "", "");

        GUILayout.BeginVertical("", "box");

		if (!Build.CanRemove)
		{
	        GUILayout.Box("Prefab Selected :");
	       	GUILayout.Box("<b><color=yellow>" + Build.Manager.Prefabs[Build.IndexSelected].transform.name + "</color></b>");
		}

        if (Build.CanRemove)
            GUILayout.Box("<b><color=red>Destroy Mode</color></b>");

		if (Build.CanBuild)
			GUILayout.Box("<b><color=cyan>Build Mode</color></b>");

        if (Build.OutOfRange)
            GUILayout.Box("<b><color=red>Out Of Range</color></b>");

        GUILayout.EndVertical();

        GUILayout.EndArea();
    }
}