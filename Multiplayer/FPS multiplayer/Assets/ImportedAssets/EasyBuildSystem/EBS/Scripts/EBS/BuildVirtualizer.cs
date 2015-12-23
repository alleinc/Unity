using UnityEngine;

/*
 * Author : Cryptoz
 * Version : 1.0
*/

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(BuildAttach))]
public class BuildVirtualizer : MonoBehaviour
{
    public Vector3 Size = new Vector3(1, 0.1f, 1);
    public Vector3 Center = Vector3.zero;
	public bool HideVirtualizer;

    private void Update()
    {
        if (GetComponent<BoxCollider>() == null)
            return;

        GetComponent<BoxCollider>().size = Size;
        GetComponent<BoxCollider>().center = Center;
    }

    private void OnDrawGizmos()
    {
        if (GetComponent<BoxCollider>() == null)
            return;

        Gizmos.color = Color.white;

        Gizmos.DrawWireCube(GetComponent<BoxCollider>().bounds.center, GetComponent<BoxCollider>().bounds.size);
    }

    private void OnDrawGizmosSelected()
    {
        if (GetComponent<Collider>() == null)
            return;

		#if UNITY_EDITOR
        UnityEditor.Handles.Label(transform.position, "Build Type : " + GetComponent<BuildAttach>().Type.ToString());
		#endif

		if(HideVirtualizer)
			return;
	
		Gizmos.color = new Color(0, 255, 0, 0.35f);

        Gizmos.DrawCube(GetComponent<BoxCollider>().bounds.center, GetComponent<BoxCollider>().bounds.size);
    }
}