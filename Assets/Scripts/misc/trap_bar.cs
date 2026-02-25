using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class trap_bar : activable
{
    public float val = 100f, timeskip = 0.01f, reduce = 0.0075f;
    public GameObject [] interactives;
    bool invoked = false;

    void discharge()
    {
        if(val - reduce > 0)
		{
			val -= reduce;
		}else
		{
			val = 0;
			CancelInvoke("discharge");
            invoked = false;
		}
    }

    void Start()
    {
        class_Start();
    } 

    void Update()
    {
        class_Update(); 
        if(active)
        {
            if(val > 0)
            {
                if(!invoked)
                {
                    InvokeRepeating("discharge", timeskip, timeskip);
                    invoked = true;
                }
            }else{
                foreach (GameObject obj in interactives)
                {
                    if (obj.GetComponent<interactive>() != null && obj.activeSelf)
                    {
                        obj.GetComponent<interactive>().active_interation = true;
                    }else if (obj.GetComponent<activable>() != null)
                    {
                        obj.GetComponent<activable>().active = true;
                    }
                    
                }
                active = false;
            }
        }else
        {
            CancelInvoke("discharge");
            invoked = false;
        }
    }
}