using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Movement")]
    public float speed = 5f;
    public float LimiteInferior = -90f; // Angulo maximo do X da camara
    public float LimiteSuperior = 90f; // Angulo minimo do X da camara

    [Header("Camera Look")]
    public float lookSpeed = 1f;
    private Vector3 rotation;

    void Start()
    {
    }

    void Update()
    {
        
        if(Input.GetMouseButton(1)){

            transform.position += speed * Time.deltaTime *transform.TransformDirection(Vector3.right)*Input.GetAxis("Horizontal");
            transform.position +=  speed*Time.deltaTime*transform.TransformDirection(Vector3.forward)*Input.GetAxis("Vertical");
            rotation.x -= Input.GetAxis("Mouse Y")*lookSpeed;
            rotation.y += Input.GetAxis("Mouse X")*lookSpeed;
            transform.rotation = Quaternion.Euler(rotation);
             if (rotation.x < LimiteInferior)
            {
                rotation.x = LimiteInferior;
            }
            else if (rotation.x > LimiteSuperior)
            {
            rotation.x = LimiteSuperior;
            } 
        }
    }
    
    public void ChangePerspective()
    {
        if(Camera.main.orthographic)
            Camera.main.orthographic = false;
        else
            Camera.main.orthographic = true;
    }
}
