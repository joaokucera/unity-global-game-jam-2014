using UnityEngine;
using System.Collections;

public class CameraBasic : MonoBehaviour
{
    public GameObject cameraTarget;
    public GameObject player;

    void Update()
    {
        transform.position = new Vector3(cameraTarget.transform.position.x, transform.position.y, transform.position.z);
    }
}
