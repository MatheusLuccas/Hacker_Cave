using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class process : MonoBehaviour
{
    public GameObject codeexe, next, icon, minimize_button;
    public Transform cam;
    public bool active = false, fixed_codeexe = false;
    Vector3 zadjust;
	AudioSource sfx;
	
    public void focus_on_this(GameObject itsme, float origin_pos = 3)
    {
        if(itsme != this.gameObject)
        {
            active = false;
            if(codeexe != null)
                codeexe.gameObject.GetComponent<codeexe>().active = false;
            if(transform.position.z < origin_pos)
            {
                zadjust = transform.position;
                zadjust.z = transform.position.z + 1;
                transform.position = zadjust;
            }
            next.gameObject.GetComponent<process>().focus_on_this(itsme, origin_pos);
        }else
        {
            active = true;
            if(codeexe != null)
                codeexe.gameObject.GetComponent<codeexe>().active = true;
        }
    }
    public void aux_focus_on_this()
    {
        next.gameObject.GetComponent<process>().focus_on_this(this.gameObject, transform.position.z);
    }
	
	void Start() 
	{
        if (gameObject.GetComponent<AudioSource>() != null)
        	sfx = gameObject.GetComponent<AudioSource>();
	}

    private void Update()
    {
        // Verifica se qualquer tecla foi pressionada
        if (active && Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2))
        {
            // Reproduz o som
            if (gameObject.GetComponent<AudioSource>() != null)
        	    sfx.Play();
        }
    }

	void OnMouseDown() 
	{   
        aux_focus_on_this();
	}
}