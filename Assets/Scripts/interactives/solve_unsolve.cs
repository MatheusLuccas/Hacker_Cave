using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class solve_unsolve : interactive
{
	
	void Update() 
	{
        interactive_Update(); 
        if((!on_connect && active_interation) || (on_connect && (connected || disconnected))){
            if(this.gameObject.GetComponent<chat>().solved)
            {
                this.gameObject.GetComponent<chat>().unsolve_all();
            }else
            {
                this.gameObject.GetComponent<chat>().solve_all();
            }
            active_interation = false;
            connected = false;
            disconnected = false;
        }
	}
}