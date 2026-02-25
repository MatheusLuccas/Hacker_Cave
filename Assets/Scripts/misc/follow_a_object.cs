using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class follow_a_object : moviment
{
    // Referência ao objeto alvo
    public Transform target;

    // Distância mínima para parar o movimento
    float stoppingDistance = 0.1f;

    void MoveTowardsTarget()
    {
        if (Vector3.Distance(transform.position, target.position) > stoppingDistance)
        {
            // Move o objeto em direção ao alvo
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void Update()
    {
        moviment_Update(); 
        // Move o objeto suavemente em direção ao alvo
        if(active)
            MoveTowardsTarget();
    }
}