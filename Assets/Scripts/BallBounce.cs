using UnityEngine;
using System.Collections;

public class BallBounce : MonoBehaviour {

	public GameObject[] Players;
	public bool IsFrozen = true;
	public float StartSpeed;
	public float speed;
	public float IncreaseOnHit;
	public float MinFreezeTime; // after this much time ball can be unfrozen by player movement
	public GameObject gameHandler;

	private ShowControlGuides showControlGuides;
	private HandleScore scoreHandler;
	private Vector3[] spawnPositions = new Vector3[] {new Vector3(5.0F, 0.0F, 0.5F), new Vector3(-5.0F, 0.0F, 0.5F)};
	private GameObject waitingFor;
	private int waitingForNum;
	private float waitingForStartPos;
	private ParticleSystem particles;
	private float freezeTime;

	void Start () {
		speed = StartSpeed;
		particles = transform.Find("BallParticle").GetComponent<ParticleSystem>();
		showControlGuides = gameHandler.GetComponent<ShowControlGuides>();
		scoreHandler = gameHandler.GetComponent<HandleScore>();

		int startPlayer;

		if (PlayerPrefs.GetString("gamemode") == "singleplayer") {

			startPlayer = 2;

		}

		else {
			startPlayer = (int)(Random.Range(1, 3) + 0.5F);  // choose start player at random
		}

		GoToSpawn(startPlayer);  // move ball next to start player's paddle
	}


	void Update () {
		// if controlling player moves their racket and minFreezeTime is elapsed, unfreeze the ball
		if (IsFrozen && 
		    (waitingFor.transform.position.y != waitingForStartPos || (PlayerPrefs.GetString("gamemode") == "singleplayer" && waitingForNum==0)) 
		     && Time.time - freezeTime > MinFreezeTime) {
			UnFreeze();  // if frozen and minFreezeTime elapsed and player moves (or is AI), unfreeze
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		// Note: 'col' holds the collision information. If the
		// Ball collided with a racket, then:
		//   col.gameObject is the racket
		//   col.transform.position is the racket's position
		//   col.collider is the racket's collider
		if (col.gameObject.name == "Player 2") {
			// Calculate hit Factor
			float y = hitFactor(transform.position,
			                    col.transform.position,
			                    col.collider.bounds.size.y);
			
			// Calculate direction, make length=1 via .normalized
			Vector2 dir = new Vector2(1, y).normalized;
			
			// Set Velocity with dir *speed
			GetComponent<Rigidbody2D>().velocity = dir *speed;
		}
		
		// Hit the right Racket?
		if (col.gameObject.name == "Player 1") {
			// Calculate hit Factor
			float y = hitFactor(transform.position,
			                    col.transform.position,
			                    col.collider.bounds.size.y);
			
			// Calculate direction, make length=1 via .normalized
			Vector2 dir = new Vector2(-1, y).normalized;
			
			// Set Velocity with dir *speed
			GetComponent<Rigidbody2D>().velocity = dir *speed;


		}

		if (col.gameObject.tag == "Player") {
			speed += IncreaseOnHit; //speed things up!
			GetComponent<AudioSource>().pitch = 1.0F;
			GetComponent<AudioSource>().Play();
			scoreHandler.PaddleWasHit(); // record score for rally mode
		}

		if (col.gameObject.tag == "Wall") {
			GetComponent<AudioSource>().pitch = 0.5F;
			GetComponent<AudioSource>().Play();
		}

	}

	float hitFactor(Vector2 ballPos, Vector2 racketPos,
	                float racketHeight) {
		// ||  1 <- at the top of the racket
		// ||
		// ||  0 <- at the middle of the racket
		// ||
		// || -1 <- at the bottom of the racket
		return (ballPos.y - racketPos.y) / racketHeight;
	}

	public void GoToSpawn (int playerNum) {
		Freeze(playerNum);
		transform.position = spawnPositions[playerNum - 1];
	}

	void Freeze (int playerNum) {
		IsFrozen = true;
		freezeTime = Time.time;
		waitingForNum = playerNum - 1;
		waitingFor = Players[waitingForNum];
		waitingForStartPos = waitingFor.transform.position.y;
		particles.enableEmission = false;
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;

		if (waitingFor.transform.position.y == 0){
			showControlGuides.ShowImages();
		}

	}

	void UnFreeze () {
		IsFrozen = false;
		GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * speed * (waitingForNum == 1 ? 1 : -1);
		particles.enableEmission = true;
		showControlGuides.HideImages();
	}
}
