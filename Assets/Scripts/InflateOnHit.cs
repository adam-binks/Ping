using UnityEngine;
using System.Collections;

public class InflateOnHit : MonoBehaviour {

	public Vector3 hitScale; // how much the object scales up on collision
	public float growSpeed;
	public float shrinkSpeed;
	public string acceptedTag; // if given, only objects with this tag will trigger the inflation

	private Vector3 baseScale; // start scale of object
	private bool isGrowing = false; // doesn't have to be growing - this is just the modification to scale. Can be < baseScale
	private bool isShrinking = false; // reversion to baseScale speed
	private float stopMargin = 0.05F; // max margin between scale and target scale to start shrinking 


	// Use this for initialization
	void Start () {
		baseScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateLerp();
	}

	// Start growing on collision
	void OnCollisionEnter2D(Collision2D col) {
		if (acceptedTag == "" || col.gameObject.tag == acceptedTag) {
			isGrowing = true;
			isShrinking = false;
		}
	}

	void UpdateLerp() {
		if (isGrowing) {
			transform.localScale = Vector3.Lerp(transform.localScale, hitScale, Time.deltaTime * growSpeed);
		}
		if (Vector3.Distance(transform.localScale, hitScale) < stopMargin) {
			isGrowing = false;
			isShrinking = true;
		}
		if (isShrinking) {
			transform.localScale = Vector3.Lerp(transform.localScale, baseScale, Time.deltaTime * shrinkSpeed);
		}
		if (transform.localScale == baseScale) {
			isShrinking = false;
		}
	}
}
