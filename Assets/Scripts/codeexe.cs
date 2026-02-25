using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class codeexe : activable
{
    public SpriteRenderer colorthis;
	public Color [] clrlist;
    //public bool active = false;
	public TMP_Text name;
    //public GameObject code_logic;
	
	void Update() 
	{
        if(active)
        {
            if(colorthis.color != clrlist[1])
            {
            colorthis.color = clrlist[1];
            }
        }else if(colorthis.color != clrlist[0])
        {
            colorthis.color = clrlist[0];
        }
	}
}

