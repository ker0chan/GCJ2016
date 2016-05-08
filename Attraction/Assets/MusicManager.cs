using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour {

	[System.Serializable]
	public struct MusicTrack
	{
		public string name;
		public AudioSource source;
	}
	public MusicTrack[] tracklist;

	Dictionary<string, MusicTrack> tracklistDictionary = new Dictionary<string, MusicTrack>();
	// Use this for initialization
	void Start () {
		foreach(MusicTrack t in tracklist)
		{
			tracklistDictionary.Add(t.name, t);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play(string name)
	{
		tracklistDictionary[name].source.Play();
	}
	public void Stop(string name)
	{
		tracklistDictionary[name].source.Stop();
	}
}
