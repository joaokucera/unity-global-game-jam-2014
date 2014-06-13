using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    public AudioClip clickButtonClip;
    public SoundManager soundManager;
    
    public Texture2D backgroundTexture;

    public GUIStyle playButtonStyle;
    public GUIStyle creditsButtonStyle;

    public float width;
    public float heigth;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);

        // PLAY
        if (GUI.Button(new Rect(Screen.width - playButtonStyle.normal.background.width * 1.075f,
                                Screen.height / 2 - playButtonStyle.normal.background.height / 2,
                                playButtonStyle.normal.background.width,
                                playButtonStyle.normal.background.height), "", playButtonStyle))
        {
            OnChangeScreen("Level1");
        }

        // CREDITS
        if (GUI.Button(new Rect(Screen.width / 2 - creditsButtonStyle.normal.background.width / 2,
                                Screen.height - creditsButtonStyle.normal.background.height,
                                creditsButtonStyle.normal.background.width,
                                creditsButtonStyle.normal.background.height), "", creditsButtonStyle))
        {
            OnChangeScreen("Credits");
        }
    }

    void OnChangeScreen(string levelName)
    {
        audio.PlayOneShot(clickButtonClip);
        soundManager.control = SoundControl.OUT;

        AutoFade.LoadLevel(levelName, 1.5f, 1.5f, Color.black);
    }
}
