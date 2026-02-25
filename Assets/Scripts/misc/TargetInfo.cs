using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetInfo : MonoBehaviour
{
    public float order = 0, time_pause = 0, change_speed = 0;
    public int index = 0;
    public string tag = "", name = "";
	public Collider2D objToTouch;
}