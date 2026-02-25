using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class follow_tags : moviment
{
    public GameObject[] targets;
    public string tagToTake = "", name = "";
    public bool randomset = false, show_the_way = false, disactive_when_end = false;
    public float spawn_time = 0.5f, fixedZPosition = 10.0f; // Valor fixo para a posição Z
    public GameObject guide; 
    public Collider2D leader;
    public SpriteRenderer mark;
    bool guide_on = false;

    void gerate_guide()
    {
        if (active && guide != null)
        {
            // Define a posição de spawn como a posição do próprio objeto
            Vector3 spawnPosition = this.transform.position;
            Quaternion spawnRotation = this.transform.rotation; // Usa a rotação do próprio objeto, se necessário

            // Instancia o novo objeto na posição e rotação especificadas
            GameObject pointer = Instantiate(guide, spawnPosition, spawnRotation);
            pointer.transform.position = new Vector3(pointer.transform.position.x, pointer.transform.position.y, fixedZPosition);
            pointer.GetComponent<follow_tags>().mark.color = mark.color;
            pointer.GetComponent<follow_tags>().targets = targets;
            pointer.GetComponent<follow_tags>().index = index;
            pointer.GetComponent<follow_tags>().loop = loop;
            pointer.GetComponent<follow_tags>().tagToTake = tagToTake;
            pointer.GetComponent<follow_tags>().randomset = randomset;
            pointer.GetComponent<follow_tags>().leader = GetComponent<Collider2D>();
        }       
    }


    int CompareTargetsByOrder(GameObject a, GameObject b)
    {
        // Comparar os valores de 'ordem' dos GameObjects
        float orderA = 0;
        float orderB = 0;
        
        if(a.GetComponent<TargetInfo>() != null && b.GetComponent<TargetInfo>())
        {
            orderA = a.GetComponent<TargetInfo>().order;
            orderB = b.GetComponent<TargetInfo>().order;
        }
        
        return orderA.CompareTo(orderB);
    }


    void takeTag(string way, int set = 0)
    {
        tagToTake = way;
        targets = GameObject.FindGameObjectsWithTag(way);
        Array.Sort(targets, CompareTargetsByOrder);
        index = set;
        //Debug.Log("vou para " + way + " indice: " + set);
    }


    void Start()
    {
		moviment_Start(); 
        if(randomset)
        {
            index = UnityEngine.Random.Range(0, targets.Length);
        }
        if(tagToTake != "" && leader == null)
            takeTag(tagToTake);
    }

    void Update()
    {
        moviment_Update();
        if(!paused){
            if(!step) ///se step, entao e preciso fazer call manualmente para ir ate o proximo ponto
            {
                call = 1; //chama call automaticamente
            }
            if (active && call>0)
            {
                if (index < targets.Length)
                {
                    aux = targets[index].transform.position;
                    aux.z = targetz;
                    direction = aux - transform.position;
                    direction.Normalize();
                    transform.Translate(direction * speed * Time.deltaTime);
                    distance = Vector3.Distance(transform.position, aux);
                    if(targets[index].GetComponent<TargetInfo>().change_speed > 0)
                    {
                        speed = targets[index].GetComponent<TargetInfo>().change_speed;
                    }
                    if (distance < 0.1f)
                    {
                        call--;
                        index++;
                        if(randomset)
                        {
                            index = UnityEngine.Random.Range(0, targets.Length);
                        }
                        if(loop && index == targets.Length)
                        {
                            index = 0;
                        }else if(index == targets.Length && disactive_when_end){
                            active = false;
                        }else if(index == targets.Length && leader != null){ //destrua guides
                            Destroy(this.gameObject);
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
            }
        }
        if(show_the_way && !guide_on)
        {
            InvokeRepeating("gerate_guide", spawn_time, spawn_time);
            guide_on = true;
        }
        else if(!show_the_way){
            CancelInvoke("gerate_guide");
            guide_on = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        moviment_OnTriggerEnter2D(col);
        if (col.gameObject.CompareTag("stop_temporary") && leader != null)
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.CompareTag("vanish_guide") && leader != null)
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.CompareTag("start_guide") && guide != null)
        {
            show_the_way = true;
        }
        if (col.gameObject.CompareTag("stop_guide") && guide != null)
        {
            show_the_way = false;
        }
        if (col.gameObject.CompareTag(tagToTake) && col.gameObject.GetComponent<TargetInfo>() != null)
        {
            if(leader == null){
                if(col.gameObject.GetComponent<TargetInfo>().time_pause > 0){
                    paused = true;
                    CancelInvoke("resume");
                    Invoke(nameof(resume), col.gameObject.GetComponent<TargetInfo>().time_pause);
                }
                /*if(col.gameObject.GetComponent<TargetInfo>().change_speed > 0){
                    speed = col.gameObject.GetComponent<TargetInfo>().change_speed;
                }*/
            }
        }
        if (col.gameObject.CompareTag("change_way") && (col.gameObject.GetComponent<TargetInfo>().objToTouch == GetComponent<Collider2D>() || col.gameObject.GetComponent<TargetInfo>().objToTouch == leader))
        {
            //if(name == "" || col.gameObject.GetComponent<TargetInfo>().name == "" || name == col.gameObject.GetComponent<TargetInfo>().name)
            paused = false;
            CancelInvoke("resume");
            takeTag(col.gameObject.GetComponent<TargetInfo>().tag, col.gameObject.GetComponent<TargetInfo>().index);
            //Destroy(col.gameObject);
            //Debug.Log("Foi um collider");
        }
    }

    /*void OnTriggerStay2D(Collider2D col)
    {
        moviment_OnTriggerEnter2D(col);
        if (col.gameObject.CompareTag(tagToTake) && col.gameObject.GetComponent<TargetInfo>() != null)
        {
            if(col.gameObject.GetComponent<TargetInfo>().time_pause > 0){
                paused = true;
                CancelInvoke("resume");
                Invoke(nameof(resume), col.gameObject.GetComponent<TargetInfo>().time_pause);
            }
            if(col.gameObject.GetComponent<TargetInfo>().change_speed > 0){
                speed = col.gameObject.GetComponent<TargetInfo>().change_speed;
            }
        }
        if (col.gameObject.CompareTag("change_way") && col.gameObject.GetComponent<TargetInfo>().objToTouch == GetComponent<Collider2D>())
        {
            //if(name == "" || col.gameObject.GetComponent<TargetInfo>().name == "" || name == col.gameObject.GetComponent<TargetInfo>().name)
            paused = false;
            CancelInvoke("resume");
            takeTag(col.gameObject.GetComponent<TargetInfo>().tag, col.gameObject.GetComponent<TargetInfo>().index);
            //Destroy(col.gameObject);
            //Debug.Log("Foi um collider");
        }
    }*/
}