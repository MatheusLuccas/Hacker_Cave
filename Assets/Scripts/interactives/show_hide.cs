using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class show_hide : interactive
{
    public GameObject [] targetList;
    public float justByTime = 0, afterTime = 0;
    public bool onePerTurn = false;
    int index = 0;
	
    void showIt(bool justOne = false)
    {
        if(justOne)
        {
            if(targetList[index] != null)
                targetList[index].SetActive(!targetList[index].activeSelf);
            index++;
            if(index == targetList.Length)
            {
                index = 0;
            }
        }else
        {
            foreach (GameObject obj in targetList)
            {
                if(obj != null)
                    obj.SetActive(!obj.activeSelf);
            }
        }
    }

    void showIt_aux(){
        showIt(onePerTurn);
    }

	void Update() 
	{
        interactive_Update(); 
        if((gameObject.activeSelf) && ((!on_connect && active_interation) || (on_connect && (connected || disconnected)))){
            
            if(afterTime > 0){
                Invoke(nameof(showIt_aux), afterTime);
            }else{
                showIt(onePerTurn);
            }
            if(justByTime > 0){
                Invoke(nameof(showIt_aux), justByTime + afterTime);
            }
            active_interation = false;
            connected = false;
            disconnected = false;
        }
	}
}