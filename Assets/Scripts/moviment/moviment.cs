using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class moviment : activable
{
    public bool loop = false, step = false;
    public float speed = 5f;
    public int index = 0, call = 0;
    public　float distance, targetz;
    public Collider2D collided;
    public Vector3 direction, zadjust, aux;
	public AudioSource sfx;
    public　bool playSong = false, paused = false;

	public void resume() 
	{
        paused = false;
	}

	public void pause(float timer) 
	{
        paused = true;
        CancelInvoke("resume");
        Invoke(nameof(resume), timer);
	}

	public void moviment_Start() 
	{
        class_Start();
        if (gameObject.GetComponent<AudioSource>() != null)
        	sfx = gameObject.GetComponent<AudioSource>();
        targetz = transform.position.z;
	}
	
	public void moviment_Update() 
	{
        class_Update(); 
	}

	void Start() 
	{
        moviment_Start(); 
	}
	
	void Update() 
	{
        moviment_Update();
    }

    public void moviment_OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("stop_temporary"))
        {
            active = false;
            collided = col;
        }
        if (col.gameObject.CompareTag("stop_for_a_time"))
        {
            col.gameObject.GetComponent<blow_in_time>().active = true;
            active = false;
            collided = col;
        }
        if (col.gameObject.CompareTag("active_chat"))
        {
            if(col.gameObject.GetComponent<chat_operator>().chat != null)
                col.gameObject.GetComponent<chat_operator>().chat.gameObject.GetComponent<chat>().walk = true;
            col.gameObject.GetComponent<chat_operator>().walk = true;
        }
        if (col.gameObject.CompareTag("solve_chat"))
        {
            if(col.gameObject.GetComponent<chat_operator>().chat != null)
                col.gameObject.GetComponent<chat_operator>().chat.gameObject.GetComponent<chat>().solve_all();
            col.gameObject.GetComponent<chat_operator>().walk = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        moviment_OnTriggerEnter2D(col);
    }


    void moviment_OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("fatal_zone"))
        {
            //this.gameObject.GetComponent<interactive>().gameover.SetActive(true); //melhorarei o gameover no futuro
            if(col.gameObject.GetComponent<chat_operator>() != null && col.gameObject.GetComponent<chat_operator>().chat != null)
                col.gameObject.GetComponent<chat_operator>().chat.gameObject.GetComponent<chat>().walk = true;
            if(col.gameObject.GetComponent<chat_operator>() != null)
                col.gameObject.GetComponent<chat_operator>().walk = true;
        }
        collided = col;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        moviment_OnTriggerStay2D(col);
    }

    void moviment_OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("stop_temporary") || col.gameObject.CompareTag("stop_for_a_time"))
        {
            active = true;
            collided = col;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        moviment_OnTriggerExit2D(col);
    }
}