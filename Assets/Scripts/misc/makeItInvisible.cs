using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class makeItInvisible : MonoBehaviour
{
    Color color;
    SpriteRenderer sprren;
    public bool invisible = true, scannable = false, awayInvisible = true;
    bool invisible_ant;
	
    public void ClassStart()
    {
        invisible_ant = !invisible;
        sprren = GetComponent<SpriteRenderer>();
        color = sprren.color;
        if(awayInvisible)
            {
                color.a = 0;
                sprren.color = color;
            }
        }

    public void ClassUpdate()
    {
        if(invisible != invisible_ant)
        {
            if(invisible)
            {
                color.a = 0;
                sprren.color = color;
            }else
            {
                color.a = 1;
                sprren.color = color;
            }
            invisible_ant = invisible;
        }
    }

    void Start()
    {
        ClassStart();
    }

    void Update()
    {
        ClassUpdate();
    }    
}