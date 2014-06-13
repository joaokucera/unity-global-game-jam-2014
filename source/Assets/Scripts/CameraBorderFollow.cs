using UnityEngine;
using System.Collections;

public class CameraBorderFollow : MonoBehaviour
{
    #region Fields

    public GameObject cameraTarget;
    public GameObject player;
    private PlayerControl playerControl;

    public float cameraHeight = 1f;
    public float smoothTime = 0.2f;
    public float borderX = 2f;
    public float borderY = 2f;

    private Vector2 velocity;
    private bool moveScreenLeft = false;
    private bool moveScreenRight = false;

    #endregion

    void Start()
    {
        cameraHeight = camera.transform.position.y;

        if (player)
        {
            playerControl = player.GetComponent<PlayerControl>();
        }
    }

    void Update()
    {
        // RIGHT
        if (cameraTarget.transform.position.x > camera.transform.position.x + borderX && playerControl.moveDirection == MoveDirection.RIGHT)
        {
            moveScreenRight = true;
        }
        if (moveScreenRight)
        {
            camera.transform.position = new Vector3(Mathf.SmoothDamp(camera.transform.position.x, camera.transform.position.x + borderX, ref velocity.y, smoothTime),
                camera.transform.position.y,
                camera.transform.position.z);
        }
        if (cameraTarget.transform.position.x < camera.transform.position.x - borderX && playerControl.moveDirection == MoveDirection.RIGHT)
        {
            moveScreenRight = false;
        }

        // LEFT
        if (cameraTarget.transform.position.x < camera.transform.position.x - borderX && playerControl.moveDirection == MoveDirection.LEFT)
        {
            moveScreenLeft = true;
        }
        if (moveScreenLeft)
        {
            camera.transform.position = new Vector3(Mathf.SmoothDamp(camera.transform.position.x, camera.transform.position.x - borderX, ref velocity.y, smoothTime),
                camera.transform.position.y,
                camera.transform.position.z);
        }
        if (cameraTarget.transform.position.x > camera.transform.position.x + borderX && playerControl.moveDirection == MoveDirection.LEFT)
        {
            moveScreenLeft = false;
        }

        camera.transform.position = new Vector3(camera.transform.position.x, cameraHeight, camera.transform.position.z);
    }
}