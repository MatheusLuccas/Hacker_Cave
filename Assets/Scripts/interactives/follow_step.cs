using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class follow_step : interactive
{
    public GameObject [] walkers;
	
    void Start()
    {
        interactive_Start();
    }
	
	void Update() 
	{
        interactive_Update(); 
        if((!on_connect && active_interation) || (on_connect && (connected || disconnected)))
        {
            foreach (GameObject obj in walkers)
            {
                if (obj.GetComponent<follow_a_way>() != null)
                    obj.GetComponent<follow_a_way>().call++;
                else if (obj.GetComponent<follow_tags>() != null)
                    obj.GetComponent<follow_tags>().call++;
            }
            active_interation = false;
            connected = false;
            disconnected = false;
        }
	}
}