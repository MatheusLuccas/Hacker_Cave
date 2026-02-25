using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class minimize_button : MonoBehaviour
{
    public GameObject process;
	AudioSource sfx;

    public void minimize_process(bool block = false){
        if(process.gameObject.GetComponent<process>().icon != null && process.gameObject.GetComponent<process>().icon.gameObject.GetComponent<icon_acess>() != null)
        {
            process.gameObject.GetComponent<process>().icon.gameObject.GetComponent<icon_acess>().targetX = process.transform.position.x;
            process.gameObject.GetComponent<process>().icon.gameObject.GetComponent<icon_acess>().targetY = process.transform.position.y;
            process.gameObject.GetComponent<process>().icon.gameObject.GetComponent<icon_acess>().active = false;
            process.gameObject.GetComponent<process>().icon.gameObject.GetComponent<icon_acess>().block = block;
        }
        process.gameObject.GetComponent<process>().icon = null;
        if(process.gameObject.GetComponent<process>().codeexe != null){
            process.gameObject.GetComponent<process>().codeexe.gameObject.GetComponent<codeexe>().active = false;
        //reinicia lógica do código
        //    if(process.gameObject.GetComponent<process>().codeexe.gameObject.GetComponent<codeexe>().code_logic != null)
        //        process.gameObject.GetComponent<process>().codeexe.gameObject.GetComponent<codeexe>().code_logic.gameObject.GetComponent<code_logic>().return_to_start();
            if(!process.gameObject.GetComponent<process>().fixed_codeexe)
                process.gameObject.GetComponent<process>().codeexe = null;
        }
        process.gameObject.GetComponent<process>().active = false;
        process.gameObject.GetComponent<follow_target>().return_to_start();
        if (gameObject.GetComponent<AudioSource>() != null)
        	sfx.Play();
    }
	
	void Start() 
	{
        if (gameObject.GetComponent<AudioSource>() != null)
        	sfx = gameObject.GetComponent<AudioSource>();
	}
	
	/*void Update() 
	{
	}*/

    void OnMouseDown()
    {
        minimize_process();
    }
}