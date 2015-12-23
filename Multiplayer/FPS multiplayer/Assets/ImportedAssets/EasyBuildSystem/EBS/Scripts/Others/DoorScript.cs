using System.Collections;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public KeyCode KeyCodeForUse = KeyCode.F;
    public float RangeForUse = 2;

    public float DoorSpeed;
    public float DoorOpenAngle = 90.0f;
    public float DoorCloseAngle = 0.0f;

    private Quaternion DoorOpen = Quaternion.identity;
    private Quaternion DoorClose = Quaternion.identity;
    private bool DoorStatus;
    private bool CanUse;

    private void Start()
    {
        DoorOpen = Quaternion.Euler(0, DoorOpenAngle, 0);
        DoorClose = Quaternion.Euler(0, DoorCloseAngle, 0);
    }

    private void OnGUI()
    {
        if (CanUse)
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 15, 200, 35), "<size=14>Press <b>" + KeyCodeForUse.ToString() + "</b> to Use</size>");
    }

    private void Update()
    {
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.position) < RangeForUse)
            CanUse = true;
        else
            CanUse = false;

        if (Input.GetKeyDown(KeyCodeForUse))
        {
            if (CanUse)
            {
                if (DoorStatus)
                    StartCoroutine(MoveDoor(DoorClose));
                else
                    StartCoroutine(MoveDoor(DoorOpen));
            }
        }
    }

    public IEnumerator MoveDoor(Quaternion dest)
    {
        while (Quaternion.Angle(transform.localRotation, dest) > 1.0f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, dest, Time.deltaTime * DoorSpeed);
            yield return null;
        }

        DoorStatus = !DoorStatus;

        yield return null;
    }
}