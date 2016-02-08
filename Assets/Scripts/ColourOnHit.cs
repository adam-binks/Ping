using UnityEngine;
using System.Collections;

public class ColourOnHit : MonoBehaviour {

	public Color hitColour; // how much the object colours up on collision
	public float changeSpeed;
	public float revertSpeed;
	public string acceptedTag; // if given, only objects with this tag will trigger the inflation
	public bool changeLight; // if true, light color will change too
	public Color lightHitColour; // light will change to this colour if changeLight=true
	
	private Color baseObjColour; // start colour of object
	private Color baseLightColour; // start colour of light
	private bool isChanging = false; // doesn't have to be growing - this is just the modification to colour. Can be < basecolour
	private bool isReverting = false; // reversion to basecolour speed
	
	
	// Use this for initialization
	void Start () {
		baseObjColour = gameObject.GetComponent<Renderer>().material.color;
		if (gameObject.GetComponent<Light>() != null) {
			baseLightColour = GetComponent<Light>().color;
		}
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Renderer>().material.color = UpdateLerp(gameObject.GetComponent<Renderer>().material.color, hitColour, baseObjColour);
		if (changeLight) {
			GetComponent<Light>().color = UpdateLerp(GetComponent<Light>().color, lightHitColour, baseLightColour);
		}
	}
	
	// Start growing on collision
	void OnCollisionEnter2D(Collision2D col) {
		if (acceptedTag == "" || col.gameObject.tag == acceptedTag) {
			isChanging = true;
			isReverting = false;
		}
	}
	
	Color UpdateLerp(Color currentColour, Color targetColour, Color baseColour) {
		if (currentColour == targetColour) {
			isChanging = false;
			isReverting = true;
		}
		if (isChanging) {
			return Color.Lerp(currentColour, targetColour, Time.deltaTime * changeSpeed);
		}
		if (currentColour == baseColour) {
			isReverting = false;
		}
		if (isReverting) {
			return Color.Lerp(currentColour, baseColour, Time.deltaTime * revertSpeed);
		}
		return currentColour;
	}
}
