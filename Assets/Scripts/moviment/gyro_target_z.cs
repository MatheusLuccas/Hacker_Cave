using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class gyro_target_z : MonoBehaviour
{
	public float angle, velocity = 300, target = 0;
	public bool active = false, following = false;
	public Transform follow_this;
	float angle_diff, direction, total_rotate, angle0, follow_z;
	Vector3 rotationPointer;

	public void return_to_start()
	{
		target = angle0;
		active = true;
	}
	
    void Start()
    {
		angle0 = transform.eulerAngles.z;
		target = angle0;
    }
	
	void Update()
	{
		angle = transform.eulerAngles.z;
		if(active)
		{			
			if(following)
			{
				follow_z = follow_this.rotation.eulerAngles.z;		
				Vector3 rotationPointer = transform.rotation.eulerAngles;
				rotationPointer.z = follow_z;
				transform.rotation = Quaternion.Euler(rotationPointer);
			}else
			{
				angle_diff = target - transform.eulerAngles.z;
				angle_diff = ((angle_diff + 180.0f) % 360.0f) - 180.0f;
				if (Mathf.Abs(angle_diff) > 0.1)
				{
					direction = Mathf.Sign(angle_diff);
					total_rotate = direction * Mathf.Min(velocity * Time.deltaTime, Mathf.Abs(angle_diff));
					transform.Rotate(Vector3.forward, total_rotate);
				}else{
					active = false;
				}
			}
			
		}
	}
}
