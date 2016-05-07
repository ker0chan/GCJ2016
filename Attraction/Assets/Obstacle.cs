using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		}
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position.x <= -21.0f) {
			Destroy (transform.gameObject);
		} 
	}

	public void SetGravityZone(int direction)
	{
		GravityZone gz = GetComponentInChildren<GravityZone>();
		if(gz != null)
		{
			gz.Show(direction);
		}
	}
}
