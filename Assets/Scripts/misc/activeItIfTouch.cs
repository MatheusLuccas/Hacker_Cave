using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class activeItIfTouch : MonoBehaviour
{
	public GameObject [] target;
	public Collider2D objToTouch;
    public string tagToTouch = "";

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((tagToTouch != "" && col.gameObject.CompareTag(tagToTouch)) || col == objToTouch)
        {
            foreach (GameObject obj in target)
            {
                if(obj.GetComponent<interactive>() != null && obj.activeSelf)
                    obj.GetComponent<interactive>().active_interation = true;
                else if(obj.GetComponent<activable>() != null){
                    obj.GetComponent<activable>().active = true;
                }
                else
                    obj.SetActive(!obj.activeSelf);
            }
        }
    }
}
