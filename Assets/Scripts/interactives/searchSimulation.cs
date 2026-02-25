using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class searchSimulation : interactive
{
    public int maxValue = 1000, helpValue = 30; // Valor máximo inicial
    public float searchInterval = 1.0f; // Intervalo de tempo entre as tentativas de busca (em segundos)
    public bool found = false;
    public GameObject[] activeAndInteract;
    bool done = false;

    void PerformSearch()
    {
        // Sorteia um valor entre 0 e maxValue
        int randomValue = Random.Range(0, maxValue + 1);
        Debug.Log("Valor sorteado: " + randomValue);

        // Verifica se o valor sorteado é 0
        if (randomValue == 0)
        {
            Debug.Log("Achou!");
            found = true;
            CancelInvoke("PerformSearch"); // Cancela a repetição da invocação
        }else{
            maxValue -= 1;
            if (maxValue < 0)
            {
                maxValue = 0;
            }
        }
    }

    // Função para reduzir o valor máximo
    public void Help(int amount)
    {
        maxValue -= amount;
        if (maxValue < 0)
        {
            maxValue = 0;
        }
        Debug.Log("Ajuda recebida, valor máximo agora é: " + maxValue);
    }

    void StartSearch()
    {
        InvokeRepeating("PerformSearch", 0, searchInterval);
    }

    void Start()
    {
        interactive_Start();
        StartSearch();
    }

	void Update() 
	{
        interactive_Update(); 
        if((!on_connect && active_interation) || (on_connect && (connected || disconnected))){
            Help(helpValue);
            active_interation = false;
            connected = false;
            disconnected = false;
        }
        if(found && !done)
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
            done = true;
        }
	}
}