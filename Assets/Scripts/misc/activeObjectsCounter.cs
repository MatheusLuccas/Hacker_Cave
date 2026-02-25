using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class activeObjectsCounter : activable
{
    public int need = 1;
    public string tagToCheck = ""; // Tag que você deseja verificar
    public GameObject[] activeAndInteract;
    int activeObjectsCount = 0;

    int CountActiveObjectsWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        int activeCount = 0;

        foreach (GameObject obj in objectsWithTag)
        {
            if (obj.activeInHierarchy) // Verifica se o objeto está ativo na hierarquia
            {
                activeCount++;
            }
        }

        return activeCount;
    }


    void Update()
    {
        if(active){
            activeObjectsCount = CountActiveObjectsWithTag(tagToCheck);
            Debug.Log("Número de objetos ativos com a tag " + tagToCheck + ": " + activeObjectsCount);
            if(need <= CountActiveObjectsWithTag(tagToCheck))
            {
                foreach (GameObject obj in activeAndInteract)
                {
                    if(obj.GetComponent<activable>() != null)
                    {
                        obj.GetComponent<activable>().active = !obj.GetComponent<activable>().active;
                    }else if(obj.GetComponent<interactive>() != null)
                    {
                        obj.GetComponent<interactive>().active_interation = true;
                    }
                }
                active = false;
            }
        }
    }
}