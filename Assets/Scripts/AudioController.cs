using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	//Audio Libraries for each catagory
	public AudioClip[] ratboySounds;
	public AudioClip[] itemSounds;

	//Singleton Stuff
	public static AudioController instance;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
			gameObject.SetActive(false);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayRatboySound(int clip)
	{
		audio.clip = ratboySounds[clip];
		audio.Play();
	}

	public void PlayItemSound(int clip)
	{
		audio.clip = itemSounds[clip];
		audio.Play();
	}
}
