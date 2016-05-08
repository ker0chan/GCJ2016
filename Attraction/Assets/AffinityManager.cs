using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AffinityManager : MonoBehaviour {

	[System.Serializable]
	public struct NPCAffinity {
		public string name;
		public int affinity;
		public AffinityLevel level;
	}

	public NPCAffinity[] affinityLevels;

	Dictionary<string, NPCAffinity> affinityLevelsDictionary = new Dictionary<string, NPCAffinity>();

	// Use this for initialization
	void Start () {
		foreach(NPCAffinity a in affinityLevels)
		{
			affinityLevelsDictionary.Add(a.name, a);
			AddAffinity(a.name, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddAffinity(string name, int amount)
	{
		NPCAffinity a = affinityLevelsDictionary[name];
		a.affinity = Mathf.Clamp(a.affinity + amount, 1, 7);
		a.level.SetAmount(a.affinity);
	}

	public int GetAffinity(string name)
	{
		return affinityLevelsDictionary[name].affinity;
	}
}
