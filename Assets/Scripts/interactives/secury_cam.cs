using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class secury_cam : twist_it
{
    public GameObject fatal_zone;


	void Update() 
	{
        class_Update();
        if(fatal_zone != null){
            if(this.gameObject.GetComponent<gyro_target_z>().active){
                fatal_zone.SetActive(true);
            }else{
                fatal_zone.SetActive(false);
            }
        }
    }
}