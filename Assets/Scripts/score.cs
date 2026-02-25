using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class score : MonoBehaviour
{
    public bool demomode = false;
    public GameObject nextTopic = null;


    void Start()
    {
        // Para cada objeto encontrado
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("BoxCommand"))
        {
            obj.GetComponent<code_logic>().demomode = demomode;
        }
    }
}