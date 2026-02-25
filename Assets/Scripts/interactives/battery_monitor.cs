using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class battery_monitor : activable
{
    public GameObject[] activeAndInteract, WhenDischarged, batteries;
    public bool all_charged = false, activated = false;

	void Update() 
	{
        class_Update();
        if(active)
        {
            all_charged = true;
            foreach (GameObject obj in batteries)
            {
                all_charged = all_charged && obj.GetComponent<battery>().charged;
            }
            if(!all_charged && activated)
            {
                foreach (GameObject obj in WhenDischarged)
                {
                    if(obj.GetComponent<activable>() != null)
                    {
                        obj.GetComponent<activable>().active = !obj.GetComponent<activable>().active;
                    }else if(obj.GetComponent<interactive>() != null)
                    {
                        obj.GetComponent<interactive>().active_interation = true;
                    }
                }
                activated = false;
            }  
            if(all_charged && !activated)
            {
                foreach (GameObject obj in activeAndInteract)
                {
                    if(obj.GetComponent<activable>() != null)
                    {
                        obj.GetComponent<activable>().active = !obj.GetComponent<activable>().active;
                    }else if(obj.GetComponent<interactive>() != null)
                    {
                        obj.GetComponent<interactive>().active_interation = true;
                    }
                }
                activated = true;
            }  
        }    
    }
}
