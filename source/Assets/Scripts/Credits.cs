using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour
{
    public AudioClip clickButtonClip;

    public SoundManager soundManager;
    public Texture2D backgroundTexture;

    public GUIStyle playButtonStyle;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);

        // MENU
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnChangeScreen("Menu");
        }

        // PLAY
        if (GUI.Button(new Rect(Screen.width - playButtonStyle.normal.background.width * 1.25f,
                                Screen.height / 2 - playButtonStyle.normal.background.height / 2,
                                playButtonStyle.normal.background.width,
                                playButtonStyle.normal.background.height), "", playButtonStyle))
        {
            OnChangeScreen("Level1");
        }
    }

    void OnChangeScreen(string levelName)
    {
        audio.PlayOneShot(clickButtonClip);
        soundManager.control = SoundControl.OUT;

        AutoFade.LoadLevel(levelName, 1.5f, 1.5f, Color.black);
    }
}
