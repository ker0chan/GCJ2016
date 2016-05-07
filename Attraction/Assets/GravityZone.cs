using UnityEngine;
using System.Collections;

public class GravityZone : MonoBehaviour {

	public Transform upEffects;
	public Transform downEffects;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void Show(int direction)
	{
		upEffects.gameObject.SetActive(false);
		downEffects.gameObject.SetActive(false);
		switch (direction)
		{
			case 1:
				upEffects.gameObject.SetActive(true);
			break;
			case -1:
				downEffects.gameObject.SetActive(true);
			break;
		}
	}
}
