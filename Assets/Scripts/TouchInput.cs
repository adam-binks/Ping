using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour {

	public GameObject[] Player = new GameObject[2];

	private Camera maincamera;


	void Start () {

		maincamera = Camera.main;

	}

	public List<string> GetTappedAreas (float distanceToMove, out List<Vector2> touchPos) {
		/* returns an array[2] containing:
		      - a list of all screen areas that are currently tapped
		      - a list of y values to jump to if dist < movevalue. otherwise contains null
		*/

		List<string> tapped = new List<string>();
		touchPos = new List<Vector2>();

		int halfScreenX = Screen.width / 2;

		foreach (Touch touch in Input.touches) {
			bool xIsRight = touch.position.x > halfScreenX;
			bool xIsTop;
			int playerNum;

			playerNum = xIsRight ? 1:0;

			float yPos = maincamera.WorldToScreenPoint(Player[playerNum].transform.position).y;

			xIsTop = (touch.position.y > yPos);

			bool isTooClose = (-distanceToMove < touch.position.y - yPos) && (touch.position.y - yPos < distanceToMove);

			tapped.Add((xIsTop ? "top":"bottom") + (xIsRight ? "right":"left") + (isTooClose ? "too close":""));
			touchPos.Add(maincamera.ScreenToWorldPoint(touch.position));
			// ^ if very close to touch position go straight there
		}

		return tapped;

	}


}