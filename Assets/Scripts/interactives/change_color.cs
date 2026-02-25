using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class change_color : interactive
{
	public Color [] clrlist;
	SpriteRenderer sprren;
	
    void Start()
    {
        interactive_Start();
        sprren = GetComponent<SpriteRenderer>();
    }
	
	void Update() 
	{
        interactive_Update(); 
        if((!on_connect && active_interation) || (on_connect && (connected || disconnected))){
            if(sprren.color == clrlist[1])
            {
                sprren.color = clrlist[0];
            }else
            {
                sprren.color = clrlist[1];
            }
            active_interation = false;
            connected = false;
            disconnected = false;
        }
	}
}