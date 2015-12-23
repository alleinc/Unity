using UnityEngine;

[RequireComponent(typeof(BuildVirtualizer))]
[ExecuteInEditMode]
public class BuildAttach : MonoBehaviour
{
    public BuildType Type = BuildType.Foundation;

	void Awake ()
	{
		switch (Type)
		{
			case BuildType.Foundation :
				gameObject.layer = LayerMask.NameToLayer("BLayerFoundation");
				break;
			case BuildType.Pillar :
				gameObject.layer = LayerMask.NameToLayer("BLayerPillar");
				break;
			case BuildType.Floor :
				gameObject.layer = LayerMask.NameToLayer("BLayerFloor");
				break;
			case BuildType.Wall :
				gameObject.layer = LayerMask.NameToLayer("BLayerWall");
				break;
			case BuildType.Stair :
				gameObject.layer = LayerMask.NameToLayer("BLayerStair");
				break;		
			case BuildType.Torch :
				gameObject.layer = LayerMask.NameToLayer("BLayerTorch");
				break;	
			default:
				gameObject.layer = LayerMask.NameToLayer("BLayer");
				break;
		}
	}
}