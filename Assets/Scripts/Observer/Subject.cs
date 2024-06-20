using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
public class Subject : MonoBehaviour
{
 
  public static event Action clean, toggleGrid;

  public static event Action<Transform> selected;

    public static void CleanScreen()
    {   
        clean?.Invoke();
    }

    public static void ToggleGrid()
    {   
        toggleGrid?.Invoke();
    }

    public static void TSelected(Transform t)
    {   
      selected?.Invoke(t);
    }


}
