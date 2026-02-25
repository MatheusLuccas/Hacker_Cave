using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class active_in_click : MonoBehaviour
{
	public GameObject [] interactives;
	AudioSource sfx;
	
    void Start() 
	{
        if (gameObject.GetComponent<AudioSource>() != null)
        	sfx = gameObject.GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        /*foreach (GameObject obj in interactives)
        {
            obj.GetComponent<interactive>().active_interation = true;
        }*/


        foreach (GameObject obj in interactives)
        {
            if (obj.GetComponent<interactive>() != null && obj.activeSelf)
            {
                obj.GetComponent<interactive>().active_interation = true;
            }else if (obj.GetComponent<activable>() != null)
            {
                obj.GetComponent<activable>().active = true;
            }
            
        }

        if (gameObject.GetComponent<AudioSource>() != null)
            sfx.Play();
    }
}