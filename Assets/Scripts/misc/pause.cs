using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pause : MonoBehaviour

{

	public bool paused = false;
	
	void Update() 
	{
		if(paused){
			Time.timeScale = 0;
		}else{
			Time.timeScale = 1;
		}
	}

    void OnMouseDown()
    {
        paused = !paused;
    }
}