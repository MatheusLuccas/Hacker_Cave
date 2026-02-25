using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class quit : MonoBehaviour
{

    void OnMouseDown()
    {
        Application.Quit();
    }
}