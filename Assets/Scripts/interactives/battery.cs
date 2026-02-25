using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class battery : interactive
{
    public float val = 0, max_val = 100, timeskip = 0.05f, increase = 35.0f, decrease = 0.5f;
	//public bool active = false;
    public bool charged = false;
    public GameObject[] activeAndInteract, WhenDischarged;
    public Transform battery_label;
	float len0;
    bool activated = false;
	
	void bar_update()
    {
        float bar_len = val / max_val;
        battery_label.localScale = new Vector3(battery_label.localScale.x, bar_len * len0, battery_label.localScale.z);
		//sprren.color = Color.Lerp(clrlist[1], clrlist[0], bar_len);
    }

    void discharge()
    {
        if(val - decrease > 0)
		{
			val -= decrease;
		}else{
			val = 0;
            charged = false;
            if(activated)
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
			CancelInvoke("discharge");
		}
        bar_update();
    }

    void recharge()
    {
        if(val + increase < max_val)
		{
			val += increase;
		}else{
			val = max_val;
            charged = true;
            if(!activated)
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
            CancelInvoke("discharge");
            InvokeRepeating("discharge", timeskip, timeskip);
		}
        //Debug.Log("carga: " + val);
        bar_update();
    }
	
    void Start() 
	{
        interactive_Start();
		len0 = battery_label.localScale.y;
        battery_label.localScale = new Vector3(battery_label.localScale.x, 0, battery_label.localScale.z);
    }

	void Update() 
	{
        interactive_Update(); 
        if((!on_connect && active_interation) || (on_connect && (connected || disconnected))){
            recharge();
            active_interation = false;
            connected = false;
            disconnected = false;
        }
	}
}
