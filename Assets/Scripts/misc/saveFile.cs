using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;

public class saveFile : MonoBehaviour
{
    public bool active;
    public int intValue = 1;
    public string campValue;

    void Update()
    {
        if(active)
        {
            if(intValue == 0 || !PlayerPrefs.HasKey(campValue) || PlayerPrefs.GetInt(campValue) < intValue)
            { //verifica se o valor é maior ou zero
                PlayerPrefs.SetInt(campValue, intValue);  
            }
            active = false;
        }
    }
}