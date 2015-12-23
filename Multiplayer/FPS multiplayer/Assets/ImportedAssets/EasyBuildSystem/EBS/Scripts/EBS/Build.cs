using System;
using UnityEngine;

/*
 * Author : Cryptoz
 * Version : 1.0
*/

[Serializable]
public class Build : MonoBehaviour
{
    public GameObject Prefab;

    public GameObject PrefabPreview;

    public float DegressRotate = 90;

    public bool CanRotate;

    public LayerMask Layer;

    public BuildType Type;
}

public enum BuildType
{
    Foundation,
    Pillar,
    Wall,
    Floor,
    Stair,
    Fence,
    Torch,
	Free
}