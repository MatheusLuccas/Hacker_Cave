using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class drag : MonoBehaviour
{
    Vector3 offset, zadjust;
    bool isDragging = false;

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    void Update()
    {
        if (isDragging)
        {
            zadjust = GetMouseWorldPosition() + offset;
            zadjust.z = 0;
            transform.position = zadjust;
        }
    }

    void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
}