using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class play_song : MonoBehaviour
{
	AudioSource sfx;
    public float loopTime = 1.5f;

    public void play_it(bool loop = false)
    {
        if (gameObject.GetComponent<AudioSource>() != null)
        {
        	sfx.Play();
            //sfx.loop = loop;
            if(loop)
                InvokeRepeating("aux_play_it", loopTime, loopTime);      
        }
    }

    void aux_play_it()
    {
        play_it(false);
    }

    public void stop_it()
    {
        if (gameObject.GetComponent<AudioSource>() != null)
        {
        	//sfx.Stop();
            //sfx.loop = false;
            CancelInvoke("aux_play_it");
        }
    }

	void Start() 
	{
        if (gameObject.GetComponent<AudioSource>() != null)
        	sfx = gameObject.GetComponent<AudioSource>();
	}
}