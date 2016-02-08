using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {


	public void LoadGameLevel(string mode) {

		PlayerPrefs.SetString("gamemode", mode);
		Application.LoadLevel("GameScene");

	}


	public void LoadMenu(string menu) {

		Application.LoadLevel(menu);

	}
}
