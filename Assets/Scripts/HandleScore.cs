using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class HandleScore : MonoBehaviour
{

	public int[] Score = new int[2] {-1, -1};
	public int RallyScore;
	public GameObject Ball;
	public Text[] ScoreTexts = new Text[2];
	private BallBounce ballBounce;

	void Awake ()
	{

		DOTween.Init (false, true, LogBehaviour.Default);

		if (PlayerPrefs.GetString ("gamemode") == "rally") {
			UpdateRallyScoreText (true);
			RallyScore = 0;
		} else {
			for (int i = 0; i <= 1; i++) {
				UpdateScoreText (i);
			}
		}

	}

	public void AddToScore (int playerNum)
	{
		// add to the player's score and restart the field of play for a new round

		Score [playerNum] += 1;

		UpdateScoreText (playerNum);

		ballBounce = Ball.GetComponent<BallBounce> ();
		ballBounce.GoToSpawn (playerNum == 1 ? 2 : 1);
		ballBounce.speed = ballBounce.StartSpeed;

		GetComponent<AudioSource>().Play ();

	}

	void UpdateScoreText (int playerNum)
	{
		// ball went out of play

		string playerText = "";

		if (PlayerPrefs.GetString ("gamemode") == "rally") {

			// update persistent highscore
			if (PlayerPrefs.GetInt ("highest_rally", 0) < RallyScore) {
				PlayerPrefs.SetInt ("highest_rally", RallyScore);
			}

			// reset score
			RallyScore = 0;

			UpdateRallyScoreText (true);

			return;
		}

		if (PlayerPrefs.GetString ("gamemode") == "singleplayer") {
			playerText = playerNum == 0 ? "AI" : "You";
		} else if (PlayerPrefs.GetString ("gamemode") == "multiplayer") {
			playerText = "P" + (playerNum + 1);
		}
		
		ScoreTexts [playerNum].text = playerText + ": " + Score [playerNum];

	}

	public void PaddleWasHit ()
	{

		if (PlayerPrefs.GetString ("gamemode") == "rally") {

			RallyScore ++;
			UpdateRallyScoreText (false);

		}

	}

	void UpdateRallyScoreText (bool updateHighscore)
	{

		ScoreTexts [0].text = "This rally: " + RallyScore;
		if (updateHighscore) { // only read from player prefs when necessary for performance
			ScoreTexts [1].text = "Best rally: " + PlayerPrefs.GetInt ("highest_rally", 0);
		}
	}
}
