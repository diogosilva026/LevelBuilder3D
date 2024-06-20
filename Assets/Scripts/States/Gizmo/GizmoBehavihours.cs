using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GizmoBehaviours : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Transform selected;

    void Start()
    {
        Subject.selected += SetObj;
    }

    private void OnMouseDown()
    {
        offset = GetWorldPoint(Input.mousePosition) - transform.position;
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = transform.position;

            if (gameObject.CompareTag("X Axis"))
            {
                newPosition = new Vector3(GetWorldPoint(Input.mousePosition).x - offset.x, transform.position.y, transform.position.z);
            }
            else if (gameObject.CompareTag("Y Axis"))
            {
                newPosition = new Vector3(transform.position.x, GetWorldPoint(Input.mousePosition).y - offset.y, transform.position.z);
            }
            else if (gameObject.CompareTag("Z Axis"))
            {
                newPosition = new Vector3(transform.position.x, transform.position.y, GetWorldPoint(Input.mousePosition, true).z - offset.z);
            }

            if (selected != null)
            {
                selected.position = newPosition;
            }
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

    private Vector3 GetWorldPoint(Vector3 screenPoint, bool forZAxis = false)
    {
        Camera cam = Camera.main;

        if (cam.orthographic)
        {
            Vector3 worldPoint = cam.ScreenToWorldPoint(screenPoint);
            if (forZAxis)
            {
                // For Z-axis movement, ensure proper depth handling for orthographic view
                return new Vector3(transform.position.x, transform.position.y, worldPoint.z);
            }
            return new Vector3(worldPoint.x, worldPoint.y, transform.position.z); // Keep z fixed for orthographic by default
        }
        else
        {
            Ray ray = cam.ScreenPointToRay(screenPoint);
            if (forZAxis)
            {
                // For Z-axis movement, the plane should be perpendicular to the Z-axis and passing through the object
                Plane plane = new Plane(Vector3.up, transform.position); // Assuming movement is perpendicular to the ground
                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 hitPoint = ray.GetPoint(distance);
                    return new Vector3(transform.position.x, transform.position.y, hitPoint.z);
                }
            }
            else
            {
                // For X and Y axis, use the default plane facing the camera
                Plane plane = new Plane(cam.transform.forward, transform.position);
                if (plane.Raycast(ray, out float distance))
                {
                    return ray.GetPoint(distance);
                }
            }
        }

        return Vector3.zero;
    }
}
