using UnityEngine;
using System.Collections;

public class CameraSmothFollow : MonoBehaviour
{
    #region Fields
    
    public GameObject cameraTarget;
    public GameObject player;
    private PlayerControl playerControl;

    public float smoothTime = 0.1f;
    public bool cameraFollowX = true;
    public bool cameraFollowY = true;
    public bool cameraFollowHeight = false;
    public float cameraHeight = 2.5f;

    public bool cameraZoom = false;
    public float cameraZoomMax = 4f;
    public float cameraZoomMin = 2.6f;
    public float cameraZoomTime = 0.03f;

    public Vector2 velocity;

    private Transform thisTransform;
    private float curPos = 0;
    private float playerJumpHeight = 0;

    #endregion

    #region Methods

    void Start()
    {
        thisTransform = transform;
        
        if (player)
        {
            playerControl = player.GetComponent<PlayerControl>();
        }
    }

    void Update()
    {
        Vector3 tranformPosition = thisTransform.position;

        if (cameraFollowX)
        {
            tranformPosition.x = Mathf.SmoothDamp(tranformPosition.x, cameraTarget.transform.position.x, ref velocity.x, smoothTime);
        }
        if (cameraFollowY)
        {
            cameraFollowHeight = false;
            tranformPosition.y = Mathf.SmoothDamp(tranformPosition.y, cameraTarget.transform.position.y, ref velocity.y, smoothTime);
        }
        if (!cameraFollowY & cameraFollowHeight)
        {
            Vector3 camPos = camera.transform.position;
            camPos.y = cameraHeight;
            camera.transform.position = camPos;
        }

        thisTransform.position = tranformPosition;

        if (cameraZoom)
        {
            curPos = player.transform.position.y;
            playerJumpHeight = curPos - playerControl.startPosY;

            if (playerJumpHeight < 0)
            {
                playerJumpHeight *= -1;
            }
            if (playerJumpHeight > cameraZoomMax)
            {
                playerJumpHeight = cameraZoomMax;
            }

            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, playerJumpHeight + cameraZoomMin, Time.time * cameraZoomTime);
        }
    }

    #endregion
}
