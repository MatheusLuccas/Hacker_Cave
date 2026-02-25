using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Speech.Synthesis;

public class chat : interactive
{
	public float up, timeskip = 7, timeskip_next = 4f, timeskip_go = 0, timeskip_again = 1f;
    public bool active = false, skipable = true, walk = false, next_invoked = false, conditional = false, solved = false;
    public TMP_Text msg;
    public GameObject invoke_next, nextTopic, unlockIt, activable; //unlockIt serve tanto para ativar quanto para desativar
    public bool waiting = false, send = false, master = false, onetime = true, gameover = false;
	public AudioSource audio;
    public bool prohibited = false;
    float time_to_return = 3f;
    SpeechSynthesizer synthesizer;
	//AudioSource sfx;


    public void hide_msg()
    {
        send = false;
        master = false;
        this.gameObject.GetComponent<follow_target>().return_to_start();
        CancelInvoke("fade_it");
        Invoke(nameof(disfade_it), timeskip);
        active = false;
        next_invoked = false;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("chat"))
        {
            obj.gameObject.GetComponent<chat>().nextTopic = null;
        }
    }


    public void fade_it()
    {
        this.gameObject.GetComponent<fade_out>().active = true;
    }


    public void disfade_it()
    {
        this.gameObject.GetComponent<fade_out>().ResetToInitialState();
    }


    public void walk_it()
    {
        walk = true;
    }


    public void show_msg()
    {

        if(!solved)
        {//Nao faca nada ate ser sua vez (garantir que conversas nao se atropelem)
            
            
            if(!prohibited && (nextTopic == null || nextTopic == this.gameObject))
            {
                send = true;
                if (gameObject.GetComponent<AudioSource>() != null)
        	        sfx.Play();
                if (audio != null && audio.clip != null)
                    audio.Play();
                if(unlockIt != null)
                    unlockIt.SetActive(!unlockIt.activeSelf);
                if(activable != null)
                    activable.gameObject.GetComponent<activable>().active = true;
                synthesizer.Speak(msg.text);
                active = true;
                waiting = false;
                foreach(GameObject obj in GameObject.FindGameObjectsWithTag("chat"))
                {
                    obj.gameObject.GetComponent<chat>().master = false;
                    //ninguem e mestre
                    obj.gameObject.GetComponent<chat>().nextTopic = invoke_next;
                    if(obj.gameObject.GetComponent<chat>().active)
                    //if(obj.gameObject.GetComponent<chat>().active && !obj.gameObject.GetComponent<chat>().solved)
                    {
                        if(!obj.gameObject.GetComponent<chat>().solved)
                        {
                            obj.gameObject.GetComponent<follow_target>().step_to(0, up);
                            //esse obj nao e mais o mestre
                        }
                        obj.gameObject.GetComponent<chat>().CancelInvoke("hide_msg");
                        obj.gameObject.GetComponent<chat>().Invoke(nameof(hide_msg), timeskip);
                        obj.gameObject.GetComponent<chat>().Invoke(nameof(fade_it), timeskip);
                        if(obj.gameObject.GetComponent<chat>().invoke_next != null && !obj.gameObject.GetComponent<chat>().next_invoked)
                        {
                            obj.gameObject.GetComponent<chat>().invoke_next.gameObject.GetComponent<chat>().Invoke(nameof(walk_it), obj.gameObject.GetComponent<chat>().timeskip_next);
                            obj.gameObject.GetComponent<chat>().next_invoked = true;
                            /*if(!audio.isPlaying)
                            {
                                obj.gameObject.GetComponent<chat>().invoke_next.gameObject.GetComponent<chat>().Invoke(nameof(walk_it), obj.gameObject.GetComponent<chat>().timeskip_go);
                                obj.gameObject.GetComponent<chat>().next_invoked = true;
                            }*/
                        }
                    }
                }
                master = true;
                //eu sou mestre
                if(onetime)
                {//evitar repetir
                    prohibited = true;
                }
                if(gameover)
                {//proiba todas as outras mensagens de serem chamadas
                    foreach(GameObject obj in GameObject.FindGameObjectsWithTag("chat"))
                    {//pede silêncio
                        if (!obj.gameObject.GetComponent<chat>().gameover)
                            obj.gameObject.GetComponent<chat>().prohibited = true;
                    }
                }
            }else{
                waiting = true;
            }
        }   
    }


    public void solve_all()
    {
        solved = true;
        if(invoke_next != null && !invoke_next.gameObject.GetComponent<chat>().solved)
        {
            invoke_next.gameObject.GetComponent<chat>().solve_all();
        }
    }


    public void unsolve_all()
    {
        solved = false;
        if(invoke_next != null && invoke_next.gameObject.GetComponent<chat>().solved)
        {
            invoke_next.gameObject.GetComponent<chat>().unsolve_all();
        }
    }
	
	void Start() 
	{
        synthesizer = new SpeechSynthesizer();
        synthesizer.SetOutputToDefaultAudioDevice();
        if (gameObject.GetComponent<AudioSource>() != null)
            sfx = gameObject.GetComponent<AudioSource>();
        if (audio.clip != null && timeskip_next == 0)
        {
            if(invoke_next != null && invoke_next != this.gameObject && invoke_next.gameObject.GetComponent<chat>().audio.clip == null)
            {
                timeskip_next = 0.5f;
                invoke_next.gameObject.GetComponent<chat>().timeskip_next = audio.clip.length - timeskip_next + 1.0f;
                if (invoke_next.gameObject.GetComponent<chat>().timeskip < invoke_next.gameObject.GetComponent<chat>().timeskip_next)
                {
                    invoke_next.gameObject.GetComponent<chat>().timeskip = invoke_next.gameObject.GetComponent<chat>().timeskip_next + 2.0f;
                }
                //Debug.Log("Audio Duration: " + timeskip_next + " seconds");
            }else
            {
                timeskip_next = audio.clip.length + 1.0f;
                if (timeskip < timeskip_next)
                {
                    timeskip = timeskip_next + 2.0f;
                }
                //Debug.Log("Audio Duration: " + timeskip_next + " seconds");

            }
        }else if(timeskip_next == 0)
            timeskip_next = 2;
	}
	
	void Update() 
	{
        if((!on_connect && active_interation) || (on_connect && (connected || disconnected)))
        {
            walk = true;
            active_interation = false;
            connected = false;
            disconnected = false;
        }
        interactive_Update();
        if(walk){
            Invoke(nameof(show_msg), timeskip_go);
            walk = false;
        }
        if(waiting){
            show_msg();
        }
	}

    void OnMouseDown()
    {
        if(master && invoke_next != null && !invoke_next.gameObject.GetComponent<chat>().send && invoke_next.gameObject.GetComponent<chat>().skipable)
        {//tem que ser o mestre (quem esta falando),
        //tem que ter topico proximo
        //o proximo topico nao pode ja ter sido invocado
        //ser skipavel

            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("chat"))
            {//pede silêncio
                if (obj.gameObject.GetComponent<chat>().audio != null && obj.gameObject.GetComponent<chat>().audio.clip != null)
                    obj.gameObject.GetComponent<chat>().audio.Stop();
            }
            invoke_next.gameObject.GetComponent<chat>().CancelInvoke("walk_it");
            invoke_next.gameObject.GetComponent<chat>().Invoke(nameof(walk_it), 0);
            next_invoked = true;
        }
    }
}