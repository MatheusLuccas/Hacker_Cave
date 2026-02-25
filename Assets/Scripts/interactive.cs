using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class interactive : MonoBehaviour
{
    public bool active_interation = false, connected = false, disconnected = false, on_connect = false, played = false, scannable = false, needaddress= false;
    public string address = "";
	public int inputa = -1, inputb = -1, inputc = -1, a_max = 4, b_max = 4, c_max = 4;
    public GameObject crypted_device, reveal;
	public AudioSource sfx;
	int [] decrypt_val = new int[3];


	public void interactive_Start() 
	{
		if (gameObject.GetComponent<AudioSource>() != null)
        	sfx = gameObject.GetComponent<AudioSource>();
		if (this.gameObject.GetComponent<crypted_device>() != null)
		{
			if(inputa == -1)
				inputa = Random.Range(0, a_max + 1);
			if(inputb == -1)
				inputb = Random.Range(0, b_max + 1);
			if(inputc == -1)
				inputc = Random.Range(0, c_max + 1);
			decrypt_val = this.gameObject.GetComponent<crypted_device>().scan(inputa, inputb, inputc, a_max, b_max, c_max);
			if(address == "")
				address = ("" + decrypt_val[0].ToString("X") + "" + decrypt_val[1].ToString("X") + "" + decrypt_val[2].ToString("X"));
		}
	}
	
	public void interactive_Update() 
	{
		if (active_interation)
		{
			if (!played && gameObject.GetComponent<AudioSource>() != null)
			{
				if(sfx != null)
					sfx.Play();		
				played = true;
			}
		}else{
			played = false;
		}
	}

	
	void Start() 
	{
        interactive_Start(); 
	}
	

	void Update() 
	{
        interactive_Update(); 
	}

}