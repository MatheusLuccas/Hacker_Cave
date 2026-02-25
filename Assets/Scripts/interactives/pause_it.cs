using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pause_it : interactive
{
	public GameObject[] targets;
    public float timer;
    void Start()
    {
        interactive_Start();
    }
	
	void Update() 
	{
        interactive_Update(); 
        if((!on_connect && active_interation) || (on_connect && (connected || disconnected))){
            foreach (GameObject obj in targets)
            {
                obj.GetComponent<moviment>().pause(timer);
            }
            active_interation = false;
            connected = false;
            disconnected = false;
        }
	}
}