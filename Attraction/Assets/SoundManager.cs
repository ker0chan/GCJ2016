using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

	public AudioSource audioSource;
	public AudioSource loopingAudioSource;

	[System.Serializable]
	public struct SFX
	{
		public string name;
		public AudioClip clip;
	}
	public SFX[] sfxs;

	Dictionary<string, SFX> sfxsDictionary = new Dictionary<string, SFX>();

	// Use this for initialization
	void Start () {
		foreach(SFX t in sfxs)
		{
			sfxsDictionary.Add(t.name, t);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Téléportation !
		/*
		if(Input.GetMouseButtonDown(1))
		{
			Play("tpstart");
		}
		if(Input.GetMouseButton(1))
		{	
			Play("tpchannel");
		}

		if(Input.GetMouseButtonUp(1))
		{
			Play("tpend");
			StopLoop();
		}
		*/
	}

	public void StopLoop()
	{
		loopingAudioSource.Stop();
	}
	public void Play(string name)
	{
		
		if(name == "tpchannel")
		{
			if(loopingAudioSource.isPlaying) return;

			loopingAudioSource.clip = sfxsDictionary[name].clip;
			loopingAudioSource.Play();
		} else 
		{
			audioSource.clip = sfxsDictionary[name].clip;
			audioSource.Play();
		}
	}
}
