using UnityEngine;
using System.Collections;

public class SpawnSaveSetup : MonoBehaviour
{
    #region Fields

    public Transform startPoint;
    public AudioClip soundDie;

    private PlayerSound soundManager;
    private Vector3 currentSavePosition;

    #endregion

    void Start()
    {
        soundManager = GetComponent<PlayerSound>();

        if (startPoint != null)
        {
            transform.position = startPoint.position;
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "SavePoint") // SAVE POINT
        {
            currentSavePosition = transform.position;
        }
        if (hit.tag == "KillBox") // KILL BOX
        {
            soundManager.PlaySound(soundDie, 0);
            transform.position = currentSavePosition;
        }
    }
}
