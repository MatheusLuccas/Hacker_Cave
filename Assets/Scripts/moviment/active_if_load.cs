using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class active_if_load : MonoBehaviour
{
	public GameObject [] interactives;
    public bool active;
    public int intValue = 1;
    public string campValue;
	AudioSource sfx;

    public void load_it()
    {
        foreach (GameObject obj in interactives)
        {
            if(obj.activeSelf)
                obj.GetComponent<interactive>().active_interation = true;
        }
        if (gameObject.GetComponent<AudioSource>() != null)
            sfx.Play();
    }
	
    void Start() 
	{
        if (gameObject.GetComponent<AudioSource>() != null)
        	sfx = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(active)
        {
            if(PlayerPrefs.HasKey(campValue))
            { //verifica se o valor equivale
                if(PlayerPrefs.GetInt(campValue) == intValue)
                {
                    load_it();
                }
            }else if(!PlayerPrefs.HasKey(campValue))
            { //verifica se o valor não existe e é zero
                if(intValue == 0)
                {
                    load_it();
                }
            }           
            active = false;
        }
    }
}