using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class activeItIfThat : MonoBehaviour
{
	public GameObject [] target;
	public GameObject leader;

	public void Update() 
	{
        foreach (GameObject obj in target)
        {
            obj.GetComponent<activable>().active = leader.GetComponent<activable>().active;
        }
	}
}
