using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour {
	
	public int PlayerNum;
	public float Speed;

	private bool PlayerIsAI = false;
	private float moveValue;
	private Vector2 startPos;
	private AIController AIControl;
	private TouchInput touchInput;

	void Awake () {
		startPos = transform.position;
		AIControl = GetComponent<AIController>();

		if (PlayerNum == 1 && PlayerPrefs.GetString("gamemode") == "singleplayer") {
			PlayerIsAI = true;
		}

		touchInput = GameObject.Find ("GameHandler").GetComponent<TouchInput>();
	}

	void FixedUpdate () {

		if (PlayerIsAI) {
			moveValue = AIControl.GetAIMoveValue();
		}

		else {
			moveValue = GetUserMoveValue();
		}


		transform.Translate(new Vector2(0, moveValue * Speed));
		transform.position = new Vector2(startPos.x, transform.position.y);
	
	}

	int GetUserMoveValue () {

		if (Application.isMobilePlatform || true) {
			// iOS

			List<Vector2> touchPos;
			List<string> tappedQuadrants = touchInput.GetTappedAreas(Speed, out touchPos);

			string side = "";

			if (PlayerNum == 1) { side = "left";}
			if (PlayerNum == 2) { side = "right";}

			if (tappedQuadrants.Count > 0) {
				for (int i=0; i < tappedQuadrants.Count; i++) {
					string quadrant = tappedQuadrants[i];

					if (quadrant.Contains("too close")) {
						// if paddle is very close to touch position go straight there
						Debug.Log(touchPos[i]);

						transform.position.Set(transform.position.x, touchPos[i].y, transform.position.z);
						return 0;
					}

					if (quadrant.Contains(side)) {
						return quadrant.Contains("top") ? 1:-1;
					}
				}
			}

			return 0;

		}

		else { 
			// standalone or web player
			return (int)Input.GetAxis("Move P" + PlayerNum);
		}
	}

}
