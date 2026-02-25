using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class button_on_off : activable
{
    void OnMouseDown()
    {
        active = !active;
    }
}