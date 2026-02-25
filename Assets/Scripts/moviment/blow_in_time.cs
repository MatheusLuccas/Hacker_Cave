using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class blow_in_time : activable
{
    public float timeskip = 10;
    public GameObject [] target;

    void blow_it()
    {
        foreach (GameObject obj in target)
        {
            if(obj.GetComponent<interactive>() != null && obj.activeSelf)
                obj.GetComponent<interactive>().active_interation = true;
            else if(obj.GetComponent<activable>() != null){
                obj.GetComponent<activable>().active = true;
            }
            else
                obj.SetActive(!obj.activeSelf);
        }
        this.gameObject.SetActive(false);
    }
	
	void Update() 
	{
        if(active){
            //Destroy(this.gameObject, timeskip);
            Invoke(nameof(blow_it), timeskip);
            active = false;            
        }
	}
}