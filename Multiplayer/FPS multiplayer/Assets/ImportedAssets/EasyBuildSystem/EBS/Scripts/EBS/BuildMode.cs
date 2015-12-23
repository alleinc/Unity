using UnityEngine;

/*
 * Author : Cryptoz
 * Version : 1.0
*/

[RequireComponent(typeof(AudioSource))]
public class BuildMode : MonoBehaviour
{
    #region Public Fields

    public KeyCode KeyForBuild = KeyCode.E;
    public KeyCode KeyForDestroy = KeyCode.D;

    public float Distance = 2;

    public Color CanBuildColor = Color.green;
    public Color CantBuildColor = Color.red;

    public AudioClip[] BuildSounds;

    [HideInInspector]
    public bool CanPlace;
	[HideInInspector]
	public bool CanRemove;
	[HideInInspector]
	public bool CanBuild;
    [HideInInspector]
    public bool OutOfRange;
    [HideInInspector]
    public int IndexSelected;

    [HideInInspector]
    public BuildManager Manager;

    #endregion

    #region Private Fields

    private BuildAttach CurrentAttach;
    private Build CurrentBuild;
    private GameObject CurrentInstance;

	private BuildAttach RemoveAttach;
	private GameObject RemoveInstance;
	private bool InstanceReady;

    private RaycastHit Hit;

    #endregion

    #region Private Methods

    private void Awake()
    {
        Manager = FindObjectOfType<BuildManager>();
    }

    private void Update()
    {
        if (!Manager)
            return;

		if (!CanBuild && !CanRemove)
		{
			float wheelAxis = Input.GetAxis("Mouse ScrollWheel");
			if (wheelAxis > 0 && IndexSelected <= Manager.Prefabs.Count - 2)
	            IndexSelected++;
			else if (wheelAxis < 0 && IndexSelected >= 1)
	            IndexSelected--;
		}

        if (Input.GetKeyUp(KeyForBuild) && !CurrentInstance)
        {
			if (CanRemove)
				return;

			OutOfRange = false;
			CanPlace = false;

            CurrentBuild = Manager.Prefabs[IndexSelected].GetComponent<Build>();
            CurrentInstance = (GameObject)Instantiate(CurrentBuild.PrefabPreview);

			ClearRemoveInstance();

            CanBuild = true;
        }

		if (Input.GetKeyUp(KeyForDestroy))
		{
			if (CanBuild)
				return;

			CanRemove = true;
		}

        #region BuildMode

        if (CanBuild)
        {
			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out Hit, 100.0f, CurrentBuild.Layer))
            {
                CurrentInstance.transform.position = Hit.point;

                if (Hit.distance < Distance)
                {
                    CanPlace = false;
                    OutOfRange = false;

					if (CurrentBuild.CanRotate)
					{
						float wheelAxis = Input.GetAxis("Mouse ScrollWheel");
						if (wheelAxis > 0)
							CurrentInstance.transform.Rotate(new Vector3(0, +CurrentBuild.DegressRotate, 0));
						else if (wheelAxis < 0)
							CurrentInstance.transform.Rotate(new Vector3(0, -CurrentBuild.DegressRotate, 0));
					}

                    CurrentAttach = Hit.collider.GetComponent<BuildAttach>();

					switch (CurrentBuild.Type)
					{
						case BuildType.Foundation:
							
							if (Hit.collider.tag == "Terrain")
							{
								CanPlace = true;
							}
							
							if (CurrentAttach != null)
							{
								if (CurrentAttach.Type == CurrentBuild.Type)
								{
									CurrentInstance.transform.position = Hit.collider.transform.position;
								CanPlace = true;
								}
							}
							
							break;
							
						case BuildType.Pillar:
							
							if (CurrentAttach != null)
							{
								if (CurrentAttach.Type == CurrentBuild.Type)
								{
									CurrentInstance.transform.position = Hit.collider.transform.position;
									CurrentInstance.transform.rotation = Hit.collider.transform.rotation;
								CanPlace = true;
								}
							}
							
							break;
							
						case BuildType.Wall:
							
							if (CurrentAttach != null)
							{
								if (CurrentAttach.Type == CurrentBuild.Type)
								{
									CurrentInstance.transform.position = Hit.collider.transform.position;
									CurrentInstance.transform.rotation = Hit.collider.transform.rotation;
								CanPlace = true;
								}
							}
							
							break;
							
						case BuildType.Floor:
							
							if (CurrentAttach != null)
							{
								if (CurrentAttach.Type == CurrentBuild.Type)
								{
									CurrentInstance.transform.position = Hit.collider.transform.position;
									
									if (!CurrentBuild.CanRotate)
									{
										CurrentInstance.transform.rotation = Hit.collider.transform.rotation;
									}
									
								CanPlace = true;
								}
							}
							
							break;
							
						case BuildType.Stair:
							
							if (CurrentAttach != null)
							{
								if (CurrentAttach.Type == CurrentBuild.Type)
								{
									CurrentInstance.transform.position = Hit.collider.transform.position;
								CanPlace = true;
								}
							}
							
							break;
							
						case BuildType.Fence:
							
							if (Hit.collider.tag == "Terrain")
							{
								CanPlace = true;
							}
							
							break;
							
						case BuildType.Torch:
							
							if (CurrentAttach != null)
							{
								if (CurrentAttach.Type == CurrentBuild.Type)
								{
									CurrentInstance.transform.position = Hit.collider.transform.position;
									CurrentInstance.transform.rotation = Hit.collider.transform.rotation;
									CanPlace = true;
								}
							}
							
							break;
							
						case BuildType.Free:
							
							if (Hit.collider.tag == "Terrain")
							{
								CanPlace = true;
							}
							
							if (CurrentAttach != null)
							{
								//if (CurrentBuild.Type != BuildType.)
								//{
									CanPlace = true;
								//}
							}
							
							break;
					}                
				}
                else
                {
                    CanPlace = false;
                    OutOfRange = true;
                }
            }
        }

        #endregion

		#region RemoveMode

		if (CanRemove)
		{
			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out Hit, Distance))
			{
				CanPlace = true;

				if (Hit.collider.transform.root.GetComponent<Build>() != null)
				{
					if (!InstanceReady)
					{
						InstanceReady = true;
						
						RemoveInstance = (GameObject)Instantiate(Hit.collider.transform.root.GetComponent<Build>().PrefabPreview);
						RemoveInstance.name = Hit.collider.transform.root.GetComponent<Build>().Prefab.name;
						
						RemoveInstance.transform.position = Hit.collider.transform.root.transform.position;
						RemoveInstance.transform.rotation = Hit.collider.transform.root.transform.rotation;
						RemoveInstance.transform.localScale += RemoveInstance.transform.localScale * 0.001f;
						
						ChangeMaterialColor(RemoveInstance, CantBuildColor);
					}

					RemoveInstance.transform.position = Hit.collider.transform.root.transform.position;

					if (Hit.collider.transform.root.GetComponent<Build>().Prefab.name != RemoveInstance.name)
						ClearRemoveInstance();
				}
			}else
				ClearRemoveInstance();
		}

		#endregion

        if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			if (CanBuild)
			{
	            if (CanPlace)
				{
	                BuildObject();
				}
			}

			if (CanRemove)
			{
				if (CanPlace)
				{
					if (Hit.collider != null)
						if (Hit.collider.transform.root.GetComponent<Build>() != null)
							RemoveObject();
				}
			}
		}

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
			OutOfRange = false;

            if (CanBuild)
            {
                Destroy(CurrentInstance);
                CanBuild = false;
            }

			if (CanRemove)
			{
				ClearRemoveInstance();
				CanRemove = false;
			}
        }

        if (CurrentInstance)
        {
            if (OutOfRange || !CanPlace)
                ChangeMaterialColor(CurrentInstance, CantBuildColor);
            else
                ChangeMaterialColor(CurrentInstance, CanBuildColor);
        }
    }

	private void ClearRemoveInstance ()
	{
		InstanceReady = false;
		Destroy (RemoveInstance);
	}

	private void ChangeMaterialColor(GameObject go, Color color)
	{
		if (go == null)	
			return;

		Renderer[] childRenderer = go.GetComponentsInChildren<Renderer>();

		foreach (Renderer child in childRenderer)
			child.material.color = color;
	}

	private void RemoveObject()
	{
		if (RemoveInstance == null)
			return;

		GetComponent<AudioSource>().PlayOneShot(BuildSounds[Random.Range(0, BuildSounds.Length)]);

		ClearRemoveInstance();
		
		Destroy(Hit.collider.transform.root.gameObject);

		OutOfRange = false;
		CanRemove = false;
	}

    private void BuildObject()
    {
		if (CurrentBuild == null)
			return;

        GetComponent<AudioSource>().PlayOneShot(BuildSounds[Random.Range(0, BuildSounds.Length)]);

        Instantiate(CurrentBuild.Prefab, CurrentInstance.transform.position, CurrentInstance.transform.rotation);

        Destroy(CurrentInstance);

        OutOfRange = false;
        CanBuild = false;
    }

    #endregion
}