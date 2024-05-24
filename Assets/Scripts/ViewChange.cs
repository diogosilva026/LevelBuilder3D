using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewChange : MonoBehaviour
{
    public void ChangePerspective()
    {
        if (Camera.main.orthographic)
            Camera.main.orthographic = false;
        else
            Camera.main.orthographic = true;
    }
}
