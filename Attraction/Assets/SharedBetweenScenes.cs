using UnityEngine;
using System.Collections.Generic;

public class SharedBetweenScenes : MonoBehaviour {

	static List<string> initedObjects = new List<string>();
	void Awake () {
		
		if(initedObjects.Exists(o => o == transform.gameObject.name))
		{
			Debug.Log("Object already exists, destroying...");
			Object.Destroy(transform.gameObject);
		} else 
		{
			initedObjects.Add(transform.gameObject.name);
			Object.DontDestroyOnLoad(transform.gameObject);
		}
	}
}
