using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class active_in_time : activable
{
	public GameObject [] interactives;
    public float timeskip = 10;

    void active_it()
    {
        foreach (GameObject obj in interactives)
        {
            if(obj.GetComponent<interactive>() != null && obj.activeSelf)
                obj.GetComponent<interactive>().active_interation = true;
            else if(obj.GetComponent<activable>() != null)
                obj.GetComponent<activable>().active = true;
            else
                obj.SetActive(!obj.activeSelf);
        }
    }
	
	void Update() 
	{
        if(active){
            //Destroy(this.gameObject, timeskip);
            Invoke(nameof(active_it), timeskip);
            active = false;            
        }
	}
}