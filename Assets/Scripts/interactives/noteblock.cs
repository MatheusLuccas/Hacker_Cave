using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class noteblock : activable
{
    public TMP_Text displayText; // Texto que será exibido na placa
    public int maxLines = 10, maxCharacters = 200; // Limites
    public string currentText = ""; // Texto atual sendo editado e sem _

    // Função para contar o número de linhas no texto atual
    int GetLineCount(string text)
    {
        return (text.Split('\n').Length - 1);
    }

    void Update()
    {
        if(active)
        {
            // Itera sobre cada tecla possível
            foreach (char c in Input.inputString)
            {
                if (c == '\b') // Se backspace é pressionado
                {
                    if (currentText.Length != 0)
                    {
                        currentText = currentText.Substring(0, currentText.Length - 1);
                    }
                }
                else if ((c == '\n') || (c == '\r')) // Se Enter é pressionado
                {
                    if (GetLineCount(currentText) < maxLines && currentText.Length < maxCharacters)
                    {
                        currentText += "\n"; // Adiciona uma nova linha
                    }
                }
                else
                {
                    if (GetLineCount(currentText) <= maxLines && currentText.Length < maxCharacters)
                    {
                        currentText += c; // Adiciona o caractere ao texto
                    }
                }

                displayText.text = currentText + '_'; // Atualiza o texto exibido
            }
        }
    }
}