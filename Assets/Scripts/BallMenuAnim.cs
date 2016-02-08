using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BallMenuAnim : MonoBehaviour {

	public float HeightToJump;
	public float Duration;

	void Awake() {
		DOTween.Init();
	}

	// Use this for initialization
	void Start () {

		gameObject.transform.DOMoveY(HeightToJump, Duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InBounce);
	}
}
