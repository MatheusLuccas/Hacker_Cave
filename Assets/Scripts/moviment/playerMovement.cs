using UnityEngine;

public class playerMovement : moviment
{
    public float xMin; // Limite mínimo no eixo x
    public float xMax; // Limite máximo no eixo x
    public float yMin; // Limite mínimo no eixo y
    public float yMax; // Limite máximo no eixo y
    float horizontal;
    float vertical;

    void Update()
    {
        moviment_Update();
        if(active)
        {
            // Obtenha a entrada do jogador
            horizontal = Input.GetAxis("Horizontal"); // A/D ou Setas Esquerda/Direita
            vertical = Input.GetAxis("Vertical"); // W/S ou Setas Cima/Baixo

            // Calcule o novo posicionamento
            Vector3 newPosition = transform.position + new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;

            // Limite o novo posicionamento ao retângulo definido
            newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
            newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax);

            // Aplique o novo posicionamento ao objeto
            transform.position = newPosition;
        }
    }
}