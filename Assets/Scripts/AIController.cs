using UnityEngine;
using System.Collections;
using DG.Tweening;


public class AIController : MonoBehaviour {

	enum Direction {
		Up = 1,
		None = 0,
		Down = -1
	}

	public float AverageReactionTime;
	public float ReactionTimeVariance; // max distance from average reaction time
	public GameObject Ball;

	private float travelDir = 0.0F;
	private float thisReactionTime;

	
	public float GetAIMoveValue () {

		float dirToMove = (float)Direction.None;

		if (Ball.transform.position.y > transform.position.y) {

			dirToMove = (float)Direction.Up;

		}

		else if (Ball.transform.position.y < transform.position.y) {

			dirToMove = (float)Direction.Down;


		}

		if (dirToMove != travelDir && (dirToMove == 1.0F || dirToMove == -1.0F)) {
			// needs to change direction of travel and is not already changing

			thisReactionTime = AverageReactionTime + Random.Range(-ReactionTimeVariance, ReactionTimeVariance);
			if (thisReactionTime < 0.01F) {
				thisReactionTime = 0.01F;
			}
			DOTween.To(()=>travelDir, x => travelDir = x, dirToMove, thisReactionTime);

		}


		return (float)travelDir;

	}
}
