using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class button_press : activable
{
    void OnMouseDown()
    {
        active = true;
    }
    void OnMouseUp()
    {
        active = false;
    }
}