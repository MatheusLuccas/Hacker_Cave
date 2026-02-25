using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class destroy_on_collide : MonoBehaviour
{
    public GameObject destroyIt;
    public string tagToDestroy;
    bool destroyed = false;


    void OnTriggerStay2D(Collider2D col)
    {
        
        if (!destroyed && (tagToDestroy == "" || col.gameObject.CompareTag(tagToDestroy)))
        {
            Destroy(destroyIt);
            destroyed = true;
        }
    }  
}