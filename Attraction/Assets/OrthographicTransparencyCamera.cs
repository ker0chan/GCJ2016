using UnityEngine;
using System.Collections;

public class OrthographicTransparencyCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera.main.transparencySortMode = TransparencySortMode.Orthographic;
	}
}
