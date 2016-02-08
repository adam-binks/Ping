using UnityEngine;
using System.Collections;

public class DontDestroyOnSceneChange : MonoBehaviour
{

	
	void Start ()
	{
		// object is not destroyed on scene change
		DontDestroyOnLoad (this);

		// if just launched keep this object
		if (PlayerPrefs.GetInt ("app just launched", 1) == 1) {
			PlayerPrefs.SetInt ("app just launched", 0);

			GetComponent<AudioSource>().Play ();
		}

		// otherwise the object already exists elsewhere so destroy this object
		else {
			Destroy (gameObject);
			GetComponent<AudioSource>().Pause ();
		}
	}

	void ResetPrefs ()
	{

		PlayerPrefs.SetInt ("app just launched", 1);

	}

	void OnApplicationPause ()
	{

		ResetPrefs ();

	}

	void OnApplicationExit ()
	{

		ResetPrefs ();

	}
	
}
