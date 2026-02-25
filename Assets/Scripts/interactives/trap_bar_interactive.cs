using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class trap_bar_interactive : interactive
{
    public GameObject [] targetList;
	
	void Update() 
	{
        interactive_Update(); 
        if((!on_connect && active_interation) || (on_connect && (connected || disconnected))){
            foreach (GameObject obj in targetList)
            {
                if(obj.GetComponent<trap_bar>().val > 0)
                    obj.GetComponent<trap_bar>().active = !obj.GetComponent<trap_bar>().active;
            }
            active_interation = false;
            connected = false;
            disconnected = false;
        }
	}
}