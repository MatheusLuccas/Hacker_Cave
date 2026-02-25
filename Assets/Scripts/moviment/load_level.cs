using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class load_level : MonoBehaviour
{
	public string destination;
    public bool active, restart;
	
    void Update() 
	{
        if (active)
        {
            if(restart)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            if(destination != "")
			    SceneManager.LoadScene(destination);
        }
    }
}