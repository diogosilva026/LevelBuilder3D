using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
public class Subject : MonoBehaviour
{
 
  public static event Action clean;

    // Update is called once per frame
    
    public static void CleanScreen()
    {   
        clean?.Invoke();
    }
}
