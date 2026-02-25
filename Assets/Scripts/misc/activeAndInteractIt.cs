using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class activeAndInteractIt : activable
{
    public GameObject[] activeAndInteract;
    public float justByTime = 0, afterTime = 0;
    bool activated = false;
	
    void activeIt()
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
    }
	
	void Update() 
	{
        class_Update();
        if(active){
            if(!activated)
            {
                if(afterTime > 0){
                    Invoke(nameof(activeIt), afterTime);
                }else{
                    activeIt();
                }
                if(justByTime > 0){
                    Invoke(nameof(activeIt), justByTime + afterTime);
                }
                activated = true;
            }
        }
	}
}
