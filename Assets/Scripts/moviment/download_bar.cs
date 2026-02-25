using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class download_bar : activable
{
	public float val = 100, max_val = 100, timeskip = 0.05f, increase = 0.5f;
	//public bool active = false;
	public GameObject [] interactives;
	float len0;
	AudioSource sfx;
	
	void bar_update()
    {
        float bar_len = val / max_val;
        transform.localScale = new Vector3(bar_len * len0, transform.localScale.y, transform.localScale.z);
		//sprren.color = Color.Lerp(clrlist[1], clrlist[0], bar_len);
    }
    void recharge()
    {
        if(val + increase < max_val)
		{
			val += increase;
		}else{
			val = max_val;
			CancelInvoke("recharge");
			if(active)
			{
				active = false;
				foreach (GameObject obj in interactives)
				{
					if(obj.activeSelf)
						obj.GetComponent<interactive>().active_interation = true;
				}
				if (gameObject.GetComponent<AudioSource>() != null)
            		sfx.Play();
			}
		}
        bar_update();
    }
	
    void Start() 
	{
        if (gameObject.GetComponent<AudioSource>() != null)
        	sfx = gameObject.GetComponent<AudioSource>();
		len0 = transform.localScale.x;
		recharge();
		InvokeRepeating("recharge", timeskip, timeskip);
		//transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
    }
}
