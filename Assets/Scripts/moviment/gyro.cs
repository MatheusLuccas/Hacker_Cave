using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class gyro : MonoBehaviour
{
	public float velocity = 360;
	public bool active = false;
	Vector2 targetPosition;

	float velocity0;
	
	public void alter_speed(float multiply)
	{
		velocity = velocity * multiply;
	}

	public void restore_velocity()
	{
		velocity = velocity0;
	}

    void Start()
    {
		velocity0 = velocity;
    }
	
	void Update()
	{
		if(active)
		{
			transform.Rotate(Vector3.up, velocity * Time.deltaTime);
		}
	}
}
