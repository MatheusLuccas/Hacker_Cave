using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class follow_target : MonoBehaviour
{
	public float velocity = 100, targetX = 0, targetY = 0, targetZ;
	public bool active = false;
	Vector3 targetPosition;
	float targetX0, targetY0, targetZ0, velocity0;
	
	public void alter_speed(float multiply)
	{
		velocity = velocity * multiply;
	}

	public void restore_velocity()
	{
		velocity = velocity0;
	}
	
	public void go_to(float stepX, float stepY, float stepZ = 0)
	{
		targetX = stepX;
		targetY = stepY;
		targetZ = stepZ;
		active = true;
	}
	
	public void step_to(float stepX, float stepY, float stepZ = 0)
	{
		targetX += stepX;
		targetY += stepY;
		targetZ += stepZ;
		active = true;
	}
	
	public void return_to_start()
	{
		targetX = targetX0;
		targetY = targetY0;
		targetZ = targetZ0;
		active = true;
	}
	
    void Start()
    {
		targetX = transform.position.x;
		targetY = transform.position.y;
		targetZ = transform.position.z;
		targetX0 = targetX;
		targetY0 = targetY;
		targetZ0 = targetZ;
		velocity0 = velocity;
    }
	
	void Update()
	{
		if(active)
		{
			targetPosition = new Vector3(targetX, targetY, targetZ);
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, velocity * Time.deltaTime);
			if(transform.position == targetPosition){
				active = false;
			}
		}
	}
}
