using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class fade_sound : MonoBehaviour
{
    // Tempo total da transição de volume
    public float fadeDuration = 1.0f;
    public bool active;
    float volumeInit, fadeTimer = 0.0f;
    //public AudioSource audioSource;

    void Start()
    {
        // Guarda o volume inicial do som
        volumeInit = AudioListener.volume;
    }

    void Update()
    {
        if(active)
        {
            // Atualiza o tempo decorrido
            fadeTimer += Time.deltaTime;

            // Calcula a interpolação linear entre o volume inicial e zero
            float t = Mathf.Clamp01(fadeTimer / fadeDuration);
            AudioListener.volume = Mathf.Lerp(volumeInit, 0f, t);

            // Se o tempo decorrido exceder o tempo total, o som é completamente silenciado
            if (fadeTimer >= fadeDuration)
            {
                // Garante que o volume seja exatamente zero
                AudioListener.volume = 0f;

                // Desativa este script para parar a transição
                enabled = false;
            }
        }
    }
}