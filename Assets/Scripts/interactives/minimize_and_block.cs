using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class minimize_and_block : interactive
{
    public bool block;
	
	void Update() 
	{
        interactive_Update(); 
        if((!on_connect && active_interation) || (on_connect && (connected || disconnected)))
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("minimize_button"))
            {
                obj.GetComponent<minimize_button>().minimize_process(block);
            }
            active_interation = false;
            connected = false;
            disconnected = false;
        }
	}
}