using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class gotoPoint : activable
{
    public Transform [] targets; // O objeto alvo para onde este objeto se moverá
    public KeyCode activationKey_next, activationKey_prev;
    public GameObject button_next,  button_prev;
    int index = 0;

    void Update()
    {
        class_Update();
        // Verifica se a tecla ou botão foi pressionado
        if (active && ((activationKey_next != null && Input.GetKeyDown(activationKey_next)) || (button_next != null && button_next.GetComponent<activable>().active)))
        {
            if(button_next != null)
                button_next.GetComponent<activable>().active = false;
            // Move este objeto para a posição do objeto alvo
            index++;
            if(index == targets.Length)
            {
                index = 0;
            }
            // Move para a posição do próximo alvo, mantendo a posição z original
            Vector3 newPosition = new Vector3(targets[index].position.x, targets[index].position.y, transform.position.z);
            transform.position = newPosition;
        }
        
        // Verifica se a tecla ou botão foi pressionado
        if (active && ((activationKey_prev != null && Input.GetKeyDown(activationKey_prev)) || (button_prev != null && button_prev.GetComponent<activable>().active)))
        {
            if(button_prev != null)
                button_prev.GetComponent<activable>().active = false;
            // Move este objeto para a posição do objeto alvo
            index--;
            if(index < 0)
            {
                index = targets.Length - 1;
            }
            // Move para a posição do alvo anterior, mantendo a posição z original
            Vector3 newPosition = new Vector3(targets[index].position.x, targets[index].position.y, transform.position.z);
            transform.position = newPosition;
        }
    }
}