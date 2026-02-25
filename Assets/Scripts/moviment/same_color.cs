using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class same_color : MonoBehaviour
{
    public SpriteRenderer copythis;
    public bool active;
	SpriteRenderer sprren;
	
	void Start() 
	{        
        sprren = GetComponent<SpriteRenderer>();
	}
	
	void Update() 
	{
        if(active && copythis != null && sprren.color != copythis.color)
		{
            sprren.color = copythis.color;
        }
	}
}