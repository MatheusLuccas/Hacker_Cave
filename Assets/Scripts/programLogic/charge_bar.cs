using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class charge_bar : activable
{
	public Color [] clrlist;
    public string [] msgstate;
	public SpriteRenderer sprren;
	public float val = 100, max_val = 100, lim_val = 200, timeskip = 0.1f, reduce = 0.1f, add = 50.0f, msgtime = 3;
	public int combo = 0, state = 0, state_limit = 0, error_count = 0, difficulty = 0;
	//public bool active = false, overclock = false;
	public bool overclock = false;
	public GameObject [] interactives;
	public GameObject overclock_button, counter_combo;
	public TMP_Text message, tiptxt;
	public AudioClip [] clip;
	float len0, reduce_increase;
	AudioSource sfx;

	void erasemsg(){
		//funcao simples para apagar a mensagem uma vez em forma de chamada
		message.text = "";
		CancelInvoke("erasemsg");
	}

	public void increase(int mult = 1)
	{
		if(active)
		{
			combo++;
			if(error_count > 0)
				error_count--;
			//descontar se tiver contador de combo
			if(counter_combo != null && counter_combo.activeSelf && counter_combo.GetComponent<counter_combo>().countdown > 0)
			{
				counter_combo.GetComponent<counter_combo>().countdown--;
			}
			if(state == -1)
			{
				message.text = "combo " + combo + "!";
				if (gameObject.GetComponent<AudioSource>() != null)
				{
        			sfx.clip = clip[2];
            		sfx.Play();
				}
			}else
			{
				state++;
				if(state > state_limit)
				{
					state = 0;
				}
				message.text = msgstate[state + 2];
				InvokeRepeating("erasemsg", msgtime, msgtime);
			}
			foreach (GameObject obj in interactives)
			{
				if(obj.activeSelf)
					obj.gameObject.GetComponent<interactive>().active_interation = true;
			}
		}
        if(val + (add*mult) < max_val)
		{
			val += (add*mult);
		}else
		{
			if(val + (add*mult) < lim_val)
			{
				val += (add*mult);
			}else
			{
				val = lim_val;
			}
			if(!active)
			{
				active = true;
				if (gameObject.GetComponent<AudioSource>() != null)
				{
        			sfx.clip = clip[0];
            		sfx.Play();
				}
				message.text = msgstate[0];
				message.color = clrlist[0];
				InvokeRepeating("erasemsg", msgtime, msgtime);
				foreach (GameObject obj in interactives)
				{
					if(obj.activeSelf)
					{
						obj.gameObject.GetComponent<interactive>().connected = true;
						obj.gameObject.GetComponent<interactive>().disconnected = false;
					}
				}
			}
		}
        bar_update();
		if(overclock_button != null)
			overclock = overclock_button.gameObject.GetComponent<button_on_off>().active;
		CancelInvoke("discharge"); //evitar multiplas chamadas (acelerar)
		if(difficulty >= 1)
		{
			if(difficulty >= 3 || active)
				InvokeRepeating("discharge", timeskip, timeskip);
		}
	}
	
	void bar_update()
    {
        float bar_len = val / max_val;
		if(bar_len > 1)
			bar_len = 1;
        transform.localScale = new Vector3(bar_len * len0, transform.localScale.y, transform.localScale.z);
		if(active)
			sprren.color = Color.Lerp(clrlist[1], clrlist[0], bar_len);
		else
			sprren.color = Color.Lerp(clrlist[1], clrlist[2], bar_len);
    }
	
    void discharge()
    {
        if(val - (reduce + (error_count * reduce_increase)) > 0)
		{
			val -= (reduce + (error_count * reduce_increase));
		}else
		{
			val = 0;
			CancelInvoke("discharge");
			if(active)
			{
				active = false;
				if (gameObject.GetComponent<AudioSource>() != null)
				{
        			sfx.clip = clip[1];
            		sfx.Play();
				}
				combo = 0;
				error_count = 0;
				if(state == -1)
				{
					message.text = "";
				}else
				{
					message.text = msgstate[1];
					message.color = clrlist[1];
					InvokeRepeating("erasemsg", msgtime, msgtime);
				}
				foreach (GameObject obj in interactives)
				{
					if(obj.activeSelf)
					{
						obj.gameObject.GetComponent<interactive>().connected = false;
						obj.gameObject.GetComponent<interactive>().disconnected = true;
					}
				}
			}
		}
        bar_update();
    }
	
    void Start() 
	{
		if(PlayerPrefs.HasKey("difficulty"))
            if(difficulty < PlayerPrefs.GetInt("difficulty"))
			{
				difficulty = PlayerPrefs.GetInt("difficulty");
            }
		if(difficulty >= 3)
			lim_val = max_val;
        if (gameObject.GetComponent<AudioSource>() != null)
        	sfx = gameObject.GetComponent<AudioSource>();
        sprren = GetComponent<SpriteRenderer>();
		if(difficulty >= 1)
		{
			if(difficulty >= 3 || active)
				InvokeRepeating("discharge", timeskip, timeskip);
		}
		len0 = transform.localScale.x;
		reduce_increase = reduce/10;
    }
	
    void Update() 
	{
		if(overclock_button != null && !overclock_button.GetComponent<button_on_off>().active){
			overclock = false;
		}
        if(overclock){
			tiptxt.color = clrlist[3];
		}
    }
}
