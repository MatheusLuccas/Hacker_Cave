using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class twist_it : interactive
{
	public float target = 0;
	
	public void class_Update() 
	{
        interactive_Update(); 
        if((!on_connect && active_interation) || (on_connect && (connected || disconnected))){
            if(this.gameObject.GetComponent<gyro_target_z>().target == target)
            {
                this.gameObject.GetComponent<gyro_target_z>().return_to_start();
            }else
            {
                this.gameObject.GetComponent<gyro_target_z>().target = target;
                this.gameObject.GetComponent<gyro_target_z>().active = true;
            }
            active_interation = false;
            connected = false;
            disconnected = false;
        }
	}
	
	void Update() 
	{
        class_Update();
    }
}