using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class show_by_time : interactive
{
    public GameObject target;
    public GameObject [] targetList;
    public float timeskip = 10;
    bool time_active = false;


    void active_caos(){
        CancelInvoke("end_active_caos");
        if(!time_active)
        {//uma vez quando liga
            foreach (GameObject obj in targetList)
            {
                obj.SetActive(!obj.activeSelf);
            }
            time_active = true;
        }
        if(timeskip > 0)
            InvokeRepeating("end_active_caos", timeskip, timeskip);
    }


    void end_active_caos(){
        CancelInvoke("end_active_caos");
        if(time_active)
        {//uma vez quando desliga
            foreach (GameObject obj in targetList)
            {
                obj.SetActive(!obj.activeSelf);
            }
            time_active = false;
        }
        if(timeskip > 0)
            InvokeRepeating("end_active_caos", timeskip, timeskip);
    }
	
	void Start() 
	{
        interactive_Start();
	}
	
	void Update() 
	{
        interactive_Update(); 
        if(active_interation){
            active_caos();
            active_interation = false;            
        }
	}
}