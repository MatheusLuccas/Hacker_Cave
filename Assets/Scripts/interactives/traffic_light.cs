using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class traffic_light : interactive
{
	public Color [] clrlist;
    public GameObject target;
    public float timeskip = 10;
	public SpriteRenderer sprren;


    void traffic_caos(){
        if (gameObject.GetComponent<AudioSource>() != null)
        	sfx.Play();
        CancelInvoke("end_traffic_caos");
        sprren.color = clrlist[1];
        target.SetActive(true);
        InvokeRepeating("end_traffic_caos", timeskip, timeskip);
    }


    void end_traffic_caos(){
        if (gameObject.GetComponent<AudioSource>() != null)
        	sfx.Play();
        CancelInvoke("end_traffic_caos");
        sprren.color = clrlist[0];
        target.SetActive(false);
        InvokeRepeating("end_traffic_caos", timeskip, timeskip);
    }
	
	void Start() 
	{
        interactive_Start();
        /*if (gameObject.GetComponent<AudioSource>() != null)
        	sfx = gameObject.GetComponent<AudioSource>();*/
	}
	
	void Update() 
	{
        interactive_Update(); 
        if(active_interation){
            traffic_caos();
            active_interation = false;            
        }
	}
}