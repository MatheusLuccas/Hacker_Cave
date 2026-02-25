using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class sprite_in_touch : MonoBehaviour
{
    // Lista de sprites que o objeto pode exibir
    public Sprite[] sprites;
    public bool active;
    int index = 0;
    SpriteRenderer spriteRenderer;

    private void updateSprite()
    {
        // Define o sprite do objeto com base no índice atual
        spriteRenderer.sprite = sprites[index];
    }

    void Start()
    {
        // Obtém a referência ao componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Define o sprite inicial
        updateSprite();
    }

    void OnMouseDown()
    {
        if(active)
        {
            // Incrementa o índice e garante que ele não ultrapasse os limites do array
            index = (index + 1) % sprites.Length;

            // Atualiza o sprite do objeto
            updateSprite();
        }
    }
}