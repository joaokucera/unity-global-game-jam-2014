using UnityEngine;
using System.Collections;

public abstract class EnemyBase : MonoBehaviour
{
    public AudioClip screemClip;
    protected int health = 10;

    public void RemoveHealth(int numberHealth)
    {
        Debug.Log("oi");

        health -= numberHealth;
    }

    void Update()
    {
        if (health <= 0)
        {
            audio.PlayOneShot(screemClip);
            Destroy(gameObject, screemClip.length);
        }

    }
}
