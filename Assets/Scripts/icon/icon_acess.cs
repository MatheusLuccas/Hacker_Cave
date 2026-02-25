using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class icon_acess : MonoBehaviour
{
    public float targetX, targetY, targetZ;
    public GameObject camPosition;
    public GameObject [] processList;
    public bool active, block = false, start_opening = false;
	public TMP_Text name;
	GameObject process;
	AudioSource sfx;

    public bool associate_process(){
        for(int i = 0; i < processList.Length; i++)
        {
            if(processList[i].gameObject.GetComponent<process>().icon == null)
            {
                process = processList[i];
                process.gameObject.GetComponent<process>().icon = this.gameObject;
                process.gameObject.GetComponent<follow_target>().go_to(targetX, targetY, targetZ);
                if(!process.gameObject.GetComponent<process>().fixed_codeexe)
                {
                    process.gameObject.GetComponent<process>().codeexe = camPosition;
                    process.gameObject.GetComponent<process>().cam.position = camPosition.transform.position;
                }
                process.gameObject.GetComponent<process>().aux_focus_on_this();
                active = true;
                if (gameObject.GetComponent<AudioSource>() != null)
        	        sfx.Play();
                return(true);
            }
        }
        //nao tem livre
        GameObject.FindWithTag("warn").GetComponent<interactive>().active_interation = true;
        return(false);
    }

    void showWindows()
    {
        if(!block)
        {
            if(!active)
                associate_process();
            else{
                process.gameObject.GetComponent<process>().aux_focus_on_this();
            }
        }
    }

	void Start() 
	{
        name.text = camPosition.gameObject.GetComponent<codeexe>().name.text;
        if (gameObject.GetComponent<AudioSource>() != null)
        	sfx = gameObject.GetComponent<AudioSource>();
	}

	void Update() 
	{
        if(start_opening)
        {
            showWindows();
            start_opening = false;
        }
	}

    void OnMouseDown()
    {
        showWindows();
    }
}