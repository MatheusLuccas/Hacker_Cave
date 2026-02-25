using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class counter_combo : activable
{
    public int countdown;
    public GameObject [] interactives;


    void Update()
    {
        class_Update(); 
        if(active && countdown == 0)
        {
            foreach (GameObject obj in interactives)
            {
                if(obj.activeSelf)
                    obj.GetComponent<interactive>().active_interation = true;
            }
            active = false;
        }
    }
}