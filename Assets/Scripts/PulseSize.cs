using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PulseSize : MonoBehaviour {

	public Ease EaseType;
	public float Duration;
	public float ScaleUp;

	// Use this for initialization
	void Start () {

		gameObject.transform.DOScale(new Vector3(ScaleUp, ScaleUp, ScaleUp), Duration).SetLoops(-1, LoopType.Yoyo).SetEase(EaseType);

	}
}
