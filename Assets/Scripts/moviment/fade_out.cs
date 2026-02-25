using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class fade_out : MonoBehaviour
{
    public float fadeDuration = 2.0f, time_to_active = 0.0f; // Duração do desvanecimento em segundos
    public bool active, destroy_after_fade = false;
    float fadeTimer = 0.0f, interpolation; // Temporizador para controlar o desvanecimento
    Renderer objectRenderer; // Referência ao componente Renderer do objeto
    Color originalColor; // Cor original do objeto
    // Referência ao TextMeshPro
    public TMP_Text msg;
    public GameObject next_to_fade;


    public void ResetToInitialState()
    {
        Color reset = Color.Lerp(originalColor, new Color(originalColor.r, originalColor.g, originalColor.b, 0), 1);
        // Define a cor do objeto de volta para a cor original
        objectRenderer.material.color = originalColor;
        if(msg != null)
            msg.color = originalColor;
        //objectRenderer.material.color = reset;

        // Reseta o temporizador de desvanecimento
        fadeTimer = 0.0f;
    }


    public void active_fade()
    {
        active = true;
    }

    void Start()
    {
        // Obtém a referência ao componente Renderer
        objectRenderer = GetComponent<Renderer>();

        // Salva a cor original do objeto
        originalColor = objectRenderer.material.color;
        if(time_to_active > 0)
        {
            Invoke(nameof(active_fade), time_to_active);
        }
    }

    void Update()
    {
        if(active)
        {
            if(next_to_fade != null)
                next_to_fade.GetComponent<fade_out>().active = true;
            
            // Incrementa o temporizador
            fadeTimer += Time.deltaTime;

            // Calcula o valor de interpolação (entre 0 e 1) com base no tempo decorrido e na duração do desvanecimento
            interpolation = fadeTimer / fadeDuration;
            
            // Se o temporizador exceder a duração do desvanecimento, destrói o objeto
            if (interpolation >= 1.0f)
            {
                active = false;
                if(destroy_after_fade){
                    Destroy(this.gameObject);
                }
            }else{

                // Interpola entre a cor original e uma cor totalmente transparente (alpha = 0)
                Color fadeColor = Color.Lerp(originalColor, new Color(originalColor.r, originalColor.g, originalColor.b, 0), interpolation);
                
                // Define a cor do objeto para a cor interpolada
                objectRenderer.material.color = fadeColor;
                if(msg != null)
                    msg.color = fadeColor;
            }
        }
    }
}