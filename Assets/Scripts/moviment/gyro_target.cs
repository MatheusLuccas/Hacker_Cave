using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class gyro_target : MonoBehaviour
{
	public float angle, velocity = 300, target = 0;
	public bool active = false, following = false;
	public Transform follow_this;
	float angle_diff, direction, total_rotate, angle0, follow_y;
	Vector3 rotationPointer;

	public void return_to_start()
	{
		target = angle0;
		active = true;
	}
	
    void Start()
    {
		angle0 = transform.eulerAngles.y;
		target = angle0;
    }
	
	void Update()
	{
		angle = transform.eulerAngles.y;
		if(active)
		{			
			if(following)
			{
				follow_y = follow_this.rotation.eulerAngles.y;		
				Vector3 rotationPointer = transform.rotation.eulerAngles;
				rotationPointer.y = follow_y;
				transform.rotation = Quaternion.Euler(rotationPointer);
			}else
			{
				angle_diff = target - transform.eulerAngles.y;
				angle_diff = ((angle_diff + 180.0f) % 360.0f) - 180.0f;
				if (Mathf.Abs(angle_diff) > 0)
				{
					direction = Mathf.Sign(angle_diff);
					total_rotate = direction * Mathf.Min(velocity * Time.deltaTime, Mathf.Abs(angle_diff));
					transform.Rotate(Vector3.up, total_rotate);
				}else{
					active = false;
				}
			}
			
		}
	}
}
