using UnityEngine;

/*
 * Author : Cryptoz
 * Version : 1.0
*/

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
public class BuildArea : MonoBehaviour
{
    public bool CanBuild = true;
    public Vector3 Size = new Vector3(1, 0.1f, 1);
    public Vector3 Center;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("BLayer");
    }

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
        if (GetComponent<BoxCollider>() == null)
            return;

        if (CanBuild)
            Gizmos.color = new Color(0, 255, 0, 0.5f);
        else
            Gizmos.color = new Color(255, 0, 0, 0.5f);

        Gizmos.DrawCube(GetComponent<BoxCollider>().bounds.center, GetComponent<BoxCollider>().bounds.size);
    }
}