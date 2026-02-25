using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class fade_in : MonoBehaviour
{
    // Tempo total da animação de aparecimento
    public float fadeDuration = 1.0f, time_to_active = 0.0f;
    public Color initialColor = Color.clear, finalColor = Color.white;
    public bool active;
    Renderer rend;
    float totalTime = 0.0f;


    public void active_fade()
    {
        active = true;
    }

    void Start()
    {
        // Obtém a referência ao componente Renderer
        rend = GetComponent<Renderer>();

        // Define a cor inicial do objeto
        rend.material.color = initialColor;
        if(time_to_active > 0)
        {
            Invoke(nameof(active_fade), time_to_active);
        }
    }

    void Update()
    {
        if(active)
        {
            // Atualiza o tempo decorrido
            totalTime += Time.deltaTime;

            // Calcula a interpolação linear entre as cores inicial e final
            float t = Mathf.Clamp01(totalTime / fadeDuration);
            rend.material.color = Color.Lerp(initialColor, finalColor, t);

            // Se o tempo decorrido exceder o tempo total, a animação termina
            if (totalTime >= fadeDuration)
            {
                // Garante que a cor final seja exatamente a cor final
                rend.material.color = finalColor;

                // Desativa este script para parar a animação
                enabled = false;
            }
        }
    }
}