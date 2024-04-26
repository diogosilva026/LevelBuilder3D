using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public GameObject Prefab;
    public void InstantiateObject()
    {
         Instantiate(Prefab, Vector3.zero, Quaternion.identity);
    }
}
