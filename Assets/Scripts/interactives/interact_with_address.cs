using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class interact_with_address : interactive
{
    public GameObject [] targetList, exceptionList;
    public GameObject noteblock, on_off;
    public bool all_if_exception = false;
    string address_to_compare = "";
    bool address_found = false;
	
	void Update() 
	{
        interactive_Update(); 
        if((!on_connect && active_interation) || (on_connect && (connected || disconnected))){
            if(on_off != null && !on_off.GetComponent<activable>().active)//funciona se alavanca ativada
            {
                address_to_compare = "";
            }else
            {
                address_to_compare = noteblock.GetComponent<noteblock>().currentText;
            }
            address_found = false;
            foreach (GameObject obj in targetList)
            {
                if(obj.GetComponent<interactive>().address.ToLower() == address_to_compare.ToLower() && obj.activeSelf)
                {
                    obj.GetComponent<interactive>().active_interation = true;
                    address_found = true;
                }
                //obj.SetActive(!obj.activeSelf);
            }
            if(!address_found)
            {
                if(all_if_exception)
                {
                    foreach (GameObject obj in targetList)
                    {
                        if(obj.activeSelf)
                            obj.GetComponent<interactive>().active_interation = true;
                    } 
                }
                foreach (GameObject obj in exceptionList)
                {
                    if(obj.activeSelf)
                        obj.GetComponent<interactive>().active_interation = true;
                }                
            }
            active_interation = false;
            connected = false;
            disconnected = false;
        }
	}
}