using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class chat_operator : MonoBehaviour
{
    public GameObject chat, activable, solve, connected, disconected, activated, icon_acess, minimize, afterCombo, walkCombo, chatOperator;
    public GameObject [] showHide;
    //public GameObject actiseve_it;
    public bool walk = false;
    public int comboToUnlock;

    public bool canUnlock()
    {
        bool result = true;
        //se o processo foi conectado
        result = result && (connected == null || connected.gameObject.GetComponent<charge_bar>().active);
        //se o processo foi desconectado
        result = result && (disconected == null || !disconected.gameObject.GetComponent<charge_bar>().active);
        //se uma ação ou um combo foi ativado
        result = result && (activated == null || (activated.gameObject.GetComponent<charge_bar>().combo > 0));
        //se um programa exe foi aberto
        result = result && (icon_acess == null || icon_acess.gameObject.GetComponent<icon_acess>().active);
        //se um processo foi minimizado
        result = result && (minimize == null || !minimize.gameObject.GetComponent<icon_acess>().active);
        //se forem feitos tantos combos
        result = result && (afterCombo == null || (afterCombo.gameObject.GetComponent<charge_bar>().combo > comboToUnlock));
        //se as condicoes de outro opeador forem validas
        result = result && (chatOperator == null || (chatOperator.gameObject.GetComponent<chat_operator>().canUnlock()));
        return result;
    }

    void Update()
    {
        if(walk && canUnlock()) //se ativo ao acordar, sempre vai checar e ativar o chat na hora que puder
        {
            if(chat != null)
                chat.gameObject.GetComponent<chat>().walk = true;
            if(solve != null)
                solve.gameObject.GetComponent<chat>().solve_all();
            if(activable != null)
                activable.gameObject.GetComponent<activable>().active = !activable.gameObject.GetComponent<activable>().active;
            foreach (GameObject obj in showHide)
            {
                obj.SetActive(!obj.activeSelf);
            }
            walk = false;
        }
    }
}