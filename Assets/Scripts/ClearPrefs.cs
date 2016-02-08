using UnityEngine;
using System.Collections;

public class ClearPrefs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteAll();
	}
}
