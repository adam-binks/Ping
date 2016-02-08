using UnityEngine;
using System.Collections;

public class GoalCollisions : MonoBehaviour {

	public GameObject GameHandler;
	public int PlayerNum;


	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.name == "Ball") {
			GameHandler.GetComponent<HandleScore>().AddToScore(PlayerNum - 1);
		}
	}
}
