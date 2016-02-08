using UnityEngine;
using System.Collections;

public class SetBallMenuPosForAspectRatio : MonoBehaviour {

	public float XPosSixteenByNine;

	// Use this for initialization
	void Awake () {

		if (Application.isMobilePlatform && AspectRatio.GetAspectRatio(Screen.width, Screen.height) != new Vector2(16, 9)) {
			Destroy(gameObject); // just destroy it god dammit screw this
		}
	}
}
