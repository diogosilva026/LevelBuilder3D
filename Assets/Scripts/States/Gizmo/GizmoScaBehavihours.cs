using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GizmoScaBehaviours : MonoBehaviour
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
        if (isDragging && selected != null)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - initialMousePosition;

            float scaleFactor = 1.0f;
            Vector3 scaleChange = Vector3.one;

            if (gameObject.CompareTag("X Axis"))
            {
                scaleFactor += mouseDelta.x * 0.01f; // Adjust scaling factor sensitivity
                scaleChange = new Vector3(scaleFactor, 1, 1);
            }
            else if (gameObject.CompareTag("Y Axis"))
            {
                scaleFactor += mouseDelta.y * 0.01f; // Adjust scaling factor sensitivity
                scaleChange = new Vector3(1, scaleFactor, 1);
            }
            else if (gameObject.CompareTag("Z Axis"))
            {
                scaleFactor += mouseDelta.x * 0.01f; // Adjust scaling factor sensitivity
                scaleChange = new Vector3(1, 1, scaleFactor);
            }

            selected.localScale = Vector3.Scale(selected.localScale, scaleChange);

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
