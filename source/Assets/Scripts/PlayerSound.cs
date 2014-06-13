using UnityEngine;
using System.Collections;

public class PlayerSound : MonoBehaviour
{
    private float soundRate = 0f;
    //private float soundDelay = 0f;

    public IEnumerator PlaySound(AudioClip soundName, float soundDelay)
    {
        if (!audio.isPlaying && Time.deltaTime > soundRate)
        {
            soundRate = Time.deltaTime * soundDelay;
            audio.clip = soundName;
            audio.Play();

            yield return new WaitForSeconds(audio.clip.length);
        }
    }
}
