using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class change_music : MonoBehaviour
{
	public bool active = false;
	public AudioClip [] clip;
    public AudioSource music;
    int track = 0;
	
	void Update()
	{
		if(active)
		{
            music.Stop();
			music.clip = clip[track];
            music.Play();
            active = false;
            if(track >= clip.Length)
                track = 0;
		}
	}
}
