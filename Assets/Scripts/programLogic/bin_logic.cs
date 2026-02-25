using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class bin_logic : MonoBehaviour
{
	public Color [] clrlist;
	public SpriteRenderer [] sprrenlist;
	public GameObject [] bin_ring;
	public TMP_Text name;
	public int val = 0, limit_val = 15, type = 0; //0 - binary; 1 - unary; 2 - true/false
	public bool active = false;
	public int inputFont = -1; //-1 quer dizer sem input, valor aleatorio

	public void alter_speed(float multiply)
	{
		bool ignoringFirst = true;
		foreach (GameObject obj in bin_ring)
        {
			if(!ignoringFirst)
			obj.gameObject.GetComponent<gyro>().alter_speed(multiply);
			ignoringFirst = false;
		}
	}

	public void restore_velocity()
	{
		bool ignoringFirst = true;
		foreach (GameObject obj in bin_ring)
        {
			if(!ignoringFirst)
			obj.gameObject.GetComponent<gyro>().restore_velocity();
			ignoringFirst = false;
		}
	}

	public void play_all()
	{
		for(int i = 0; i < 4; i++)
		if(sprrenlist[i].color == clrlist[1])
		{
			bin_ring[i].gameObject.GetComponent<play_song>().play_it(true);
		}
	}

	public void stop_all()
	{
		foreach (GameObject obj in bin_ring)
        {
			obj.gameObject.GetComponent<play_song>().stop_it();
		}
	}
	

	void colorBit()
	{
		int calColor = val;
		if(active){
			if(type == 0)
			{
				calColor = val;
			}else if(type == 1)
			{
				calColor = Convert.ToInt32(Math.Pow(2, val)) - 1;
			}else
			{
				calColor = val * 15;
			}
			if(calColor >= 8){
				if(sprrenlist[3].color != clrlist[1])
				{
					sprrenlist[3].color = clrlist[1];
					bin_ring[3].gameObject.GetComponent<play_song>().play_it();
				}
				calColor -= 8;
			}else{
				sprrenlist[3].color = clrlist[0];
			}
			if(calColor >= 4){
				if(sprrenlist[2].color != clrlist[1])
				{
					sprrenlist[2].color = clrlist[1];
					bin_ring[2].gameObject.GetComponent<play_song>().play_it();
				}
				calColor -= 4;

			}else{
				sprrenlist[2].color = clrlist[0];
			}
			if(calColor >= 2){
				if(sprrenlist[1].color != clrlist[1])
				{
					sprrenlist[1].color = clrlist[1];
					bin_ring[1].gameObject.GetComponent<play_song>().play_it();
				}
				sprrenlist[1].color = clrlist[1];
				calColor -= 2;
			}else{
				sprrenlist[1].color = clrlist[0];
			}
			if(calColor == 1)
			{
				if(sprrenlist[0].color != clrlist[1])
				{
					sprrenlist[0].color = clrlist[1];
					bin_ring[0].gameObject.GetComponent<play_song>().play_it();
				}
			}else{
				sprrenlist[0].color = clrlist[0];
			}
			
		}else{
			sprrenlist[0].color = clrlist[2];
			sprrenlist[1].color = clrlist[2];
			sprrenlist[2].color = clrlist[2];
			sprrenlist[3].color = clrlist[2];
		}
		rotateBit();
	}
	

	void rotateBit()
	{
		int calColor = val;
		if(active){
			if(type == 0)
			{
				calColor = val;
			}else if(type == 1)
			{
				calColor = Convert.ToInt32(Math.Pow(2, val)) - 1;
			}else
			{
				calColor = val * 15;
			}
			if(calColor >= 8){
				bin_ring[3].gameObject.GetComponent<gyro>().active = true;
				bin_ring[3].gameObject.GetComponent<gyro_target>().active = false;
				calColor -= 8;
			}else{
				bin_ring[3].gameObject.GetComponent<gyro>().active = false;
				bin_ring[3].gameObject.GetComponent<gyro_target>().target = 0;
				bin_ring[3].gameObject.GetComponent<gyro_target>().active = true;
			}
			if(calColor >= 4){
				if(type == 0 || !bin_ring[3].gameObject.GetComponent<gyro>().active)
				{
					bin_ring[2].gameObject.GetComponent<gyro>().active = true;
					bin_ring[2].gameObject.GetComponent<gyro_target>().active = false;
					bin_ring[2].gameObject.GetComponent<gyro_target>().following = false;
				}else
				{
					bin_ring[2].gameObject.GetComponent<gyro_target>().active = true;
					bin_ring[2].gameObject.GetComponent<gyro_target>().following = true;
				}
				calColor -= 4;

			}else{
				bin_ring[2].gameObject.GetComponent<gyro>().active = false;
				bin_ring[2].gameObject.GetComponent<gyro_target>().target = 0;
				bin_ring[2].gameObject.GetComponent<gyro_target>().active = true;
				bin_ring[2].gameObject.GetComponent<gyro_target>().following = false;
			}
			if(calColor >= 2){
				if(type == 0 || (!bin_ring[3].gameObject.GetComponent<gyro>().active && !bin_ring[2].gameObject.GetComponent<gyro>().active))
				{
					bin_ring[1].gameObject.GetComponent<gyro>().active = true;
					bin_ring[1].gameObject.GetComponent<gyro_target>().active = false;
					bin_ring[1].gameObject.GetComponent<gyro_target>().following = false;
				}else
				{
					bin_ring[1].gameObject.GetComponent<gyro_target>().active = true;
					bin_ring[1].gameObject.GetComponent<gyro_target>().following = true;
				}
				calColor -= 2;
			}else{
				bin_ring[1].gameObject.GetComponent<gyro>().active = false;
				bin_ring[1].gameObject.GetComponent<gyro_target>().target = 0;
				bin_ring[1].gameObject.GetComponent<gyro_target>().active = true;
				bin_ring[1].gameObject.GetComponent<gyro_target>().following = false;
			}
			
		}else{
			bin_ring[1].gameObject.GetComponent<gyro>().active = false;
			bin_ring[1].gameObject.GetComponent<gyro_target>().active = true;
			bin_ring[1].gameObject.GetComponent<gyro_target>().following = false;
			bin_ring[2].gameObject.GetComponent<gyro>().active = false;
			bin_ring[2].gameObject.GetComponent<gyro_target>().active = true;
			bin_ring[2].gameObject.GetComponent<gyro_target>().following = false;
			bin_ring[3].gameObject.GetComponent<gyro>().active = false;
			bin_ring[3].gameObject.GetComponent<gyro_target>().active = true;
			bin_ring[3].gameObject.GetComponent<gyro_target>().following = false;
		}
	}
	
	void Update()
	{
		colorBit();
	}
}
