using UnityEngine;
using System.Collections;

public class Credits2 : MonoBehaviour
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
	}
	
	void OnChangeScreen(string levelName)
	{
		audio.PlayOneShot(clickButtonClip);
		soundManager.control = SoundControl.OUT;
		
		AutoFade.LoadLevel(levelName, 1.5f, 1.5f, Color.black);
	}
}
