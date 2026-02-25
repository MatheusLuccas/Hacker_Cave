using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class follow_a_way : moviment
{
    public Transform[] targets;

    void Update()
    {  
        moviment_Update(); 
        if(!step) ///se step, entao e preciso fazer call manualmente para ir ate o proximo ponto
        {
            call = 1; //chama call automaticamente
        }
        if (active && call>0)
        {
            if (index < targets.Length)
            {
                aux = targets[index].position;
                aux.z = targetz;
                direction = aux - transform.position;
                direction.Normalize();
                transform.Translate(direction * speed * Time.deltaTime);
                distance = Vector3.Distance(transform.position, aux);
                if (distance < 0.1f)
                {
                    call--;
                    index++;
                    if(loop && index == targets.Length)
                    {
                        index = 0;
                    }
                    playSong = false;
                }else if(!playSong){
                    if (gameObject.GetComponent<AudioSource>() != null)
        	            sfx.Play();
                    playSong = true;
                }
                /*zadjust = transform.position;
                zadjust.z = targetz;
                transform.position = zadjust;*/
            }
        }
        if(collided != null)
        {
            if((collided.gameObject.CompareTag("stop_temporary")||collided.gameObject.CompareTag("stop_for_a_time")) && !collided.gameObject.activeSelf)
            {
                active = true;
            }
        }/*else
        {
            active = true;
        }*/
    }
}