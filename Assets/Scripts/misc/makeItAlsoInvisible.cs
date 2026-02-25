using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class makeItAlsoInvisible : makeItInvisible
{
    public GameObject copyThis;

    void Update()
    {
        invisible = copyThis.gameObject.GetComponent<makeItInvisible>().invisible;
        ClassUpdate();
    }    
}