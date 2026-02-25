using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class active_if_load_by_priority : activable
{
	public GameObject [] interactives;
    //public int intValue = 1;
    public string [] campValue;
	AudioSource sfx;
    int index;
    bool found = false;

    public void load_it()
    {
        Debug.Log("INDEX: " + index);
        if(interactives[index].activeSelf)
        {
            interactives[index].GetComponent<interactive>().active_interation = true;
            Debug.Log("interagiu");    
        }
        if (gameObject.GetComponent<AudioSource>() != null)
            sfx.Play();
        found = true;
    }
	
    void Start() 
	{
        class_Start();
        if (gameObject.GetComponent<AudioSource>() != null)
        	sfx = gameObject.GetComponent<AudioSource>();
        index = interactives.Length - 1;
    }

    void Update()
    {
        class_Update();
        if(active)
        {
            while(index >= 0 && !found)
            {
                Debug.Log("Entrou aqui!");
                if(PlayerPrefs.HasKey(campValue[index]))
                { //verifica se o valor equivale
                    Debug.Log("Aqui também!");
                    if(PlayerPrefs.GetInt(campValue[index]) > 0)
                    {
                        Debug.Log("Aqui duvido!");
                        load_it();
                    }
                }/*else if(!PlayerPrefs.HasKey(campValue[index]))
                { //verifica se o valor não existe e é zero
                    if(intValue == 0)
                    {
                        load_it();
                    }
                }*/
                index--;
            }           
            active = false;
        }
        if(!found)
            Debug.Log("Nao achou!");
    }
}