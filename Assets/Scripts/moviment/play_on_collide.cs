using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class play_on_collide : MonoBehaviour
{
    public string tagToPlay;
	AudioSource sfx;
    bool played = false;


	void Start() 
	{
        if (gameObject.GetComponent<AudioSource>() != null)
        	sfx = gameObject.GetComponent<AudioSource>();
	} 


    void OnTriggerStay2D(Collider2D col)
    {
        
        if (!played && (tagToPlay == "" || col.gameObject.CompareTag(tagToPlay)))
        {
            if (gameObject.GetComponent<AudioSource>() != null)
            {
        	    sfx.Play();
                played = true;
            }
        }
    }  
}