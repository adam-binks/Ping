using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowControlGuides : MonoBehaviour
{

	public Image[] ControlImgsKeyboard = new Image[2];
	public Image[] ControlImgsIOS = new Image[2];
	private Image[] ControlImgs;

	void Start ()
	{
		// disable all control guides then selectively enable them
		foreach (Image img in ControlImgsKeyboard) {
			img.enabled = false;
		}
		foreach (Image img in ControlImgsIOS) {
			img.enabled = false;
		}

		if (Application.platform == RuntimePlatform.IPhonePlayer) {

			ControlImgs = ControlImgsIOS;

		} else {

			ControlImgs = ControlImgsKeyboard;
			
		}

		ShowImages ();

	}

	public void ShowImages ()
	{
		// if multiplayer show only player 2's control guide
		if (PlayerPrefs.GetString("gamemode") == "singleplayer") {
			ControlImgs [0].enabled = false;
			ControlImgs [1].enabled = true;
		} else {
			// otherwise show both
			foreach (Image img in ControlImgs) {
				img.enabled = true;
			}
		}
	}

	public void HideImages ()
	{
		foreach (Image img in ControlImgs) {
			img.enabled = false;
		}
	}
}
