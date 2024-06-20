using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GizmoRotBehaviours : MonoBehaviour
{
    private Vector3 initialMousePosition;
    private bool isDragging = false;
    private Transform selected;

    void Start()
    {
        Subject.selected += SetObj;
    }

    private void OnMouseDown()
    {
        initialMousePosition = Input.mousePosition;
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - initialMousePosition;

            float angle = mouseDelta.magnitude;
            Vector3 axis = Vector3.zero;

            if (gameObject.CompareTag("X Axis"))
            {
                axis = Vector3.right;
                angle = mouseDelta.y; // Use vertical mouse movement for X-axis rotation
            }
            else if (gameObject.CompareTag("Y Axis"))
            {
                axis = Vector3.up;
                angle = mouseDelta.x; // Use horizontal mouse movement for Y-axis rotation
            }
            else if (gameObject.CompareTag("Z Axis"))
            {
                axis = Vector3.forward;
                angle = mouseDelta.x; // Use horizontal mouse movement for Z-axis rotation
            }

            if (axis != Vector3.zero)
            {
                transform.Rotate(axis, angle, Space.World);
                if (selected != null)
                {
                    selected.Rotate(axis, angle, Space.World);
                }
            }

            initialMousePosition = currentMousePosition; // Update the initial mouse position for the next frame
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void SetObj(Transform t)
    {
        selected = t;
    }
}
