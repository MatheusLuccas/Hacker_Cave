using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class needItToActiveInteract : activable
{
    public GameObject[] need;
    public GameObject[] activeAndInteract;
    bool canactive = false;

	void Start() 
	{
        class_Start(); 
	}
	
	void Update() 
	{
        class_Update();
        if(active)
        {
            canactive = true;
            foreach (GameObject obj in need)
            {
                if((obj.GetComponent<activable>() != null && !obj.GetComponent<activable>().active) || (obj.GetComponent<interactive>() != null && !obj.GetComponent<interactive>().active_interation))
                {
                    canactive = false;
                }
            }
            foreach (GameObject obj in activeAndInteract)
            {
                if(obj.GetComponent<activable>() != null)
                {
                    obj.GetComponent<activable>().active = canactive;
                }else if(obj.GetComponent<interactive>() != null)
                {
                    obj.GetComponent<interactive>().active_interation = canactive;
                }
            }
        }
	}
}

