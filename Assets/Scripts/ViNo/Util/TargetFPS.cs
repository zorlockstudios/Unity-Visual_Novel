using UnityEngine;
using System.Collections;

public class TargetFPS : MonoBehaviour {
	public int targetFrameRate;

	// Use this for initialization
	void Awake () {
		Application.targetFrameRate = targetFrameRate;
	}
	
}
