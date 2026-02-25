using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class radio : interactive
{
    public GameObject [] var;
    public Color [] clrlist = new Color[2];
	SpriteRenderer sprren;
    GameObject scanned;
	
    void Start()
    {
        interactive_Start();
        sprren = GetComponent<SpriteRenderer>();
        clrlist[0] = sprren.color;
    }
	
	void Update() 
	{
        interactive_Update(); 
        if((!on_connect && active_interation) || (on_connect && (connected || disconnected))){
            if(scanned != null)
            {
                scanned.gameObject.GetComponent<makeItInvisible>().invisible = false;
                if(scanned.gameObject.GetComponent<interactive>() != null && scanned.gameObject.GetComponent<interactive>().reveal != null)
                {
                    scanned.gameObject.GetComponent<interactive>().reveal.SetActive(true);
                }
            }
            active_interation = false;
        }
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<interactive>() != null && col.gameObject.GetComponent<interactive>().scannable)
        {
            sprren.color = clrlist[1];
            var[0].GetComponent<bin_logic>().inputFont = col.gameObject.GetComponent<interactive>().inputa;
            var[1].GetComponent<bin_logic>().inputFont = col.gameObject.GetComponent<interactive>().inputb;
            var[2].GetComponent<bin_logic>().inputFont = col.gameObject.GetComponent<interactive>().inputc;
            

            if (col.gameObject.GetComponent<makeItInvisible>() != null && !col.gameObject.GetComponent<makeItInvisible>().awayInvisible)
            {
                scanned = col.gameObject;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<interactive>() != null)
        {
            sprren.color = clrlist[0];
        }
    }
}