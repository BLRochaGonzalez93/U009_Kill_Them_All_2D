using UnityEngine;

public class TwisterOrientation : MonoBehaviour
{
    private void Start()
    {
        Vector3 rot = transform.rotation.eulerAngles * -1;
        transform.GetChild(0).transform.Rotate(rot);
    }

}
