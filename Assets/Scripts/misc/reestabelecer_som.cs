using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class reestabelecer_som : MonoBehaviour
{
    public float volumePadrao = 1.0f;

    void Start()
    {
        AudioListener.volume = volumePadrao;
    }    
}