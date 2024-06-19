
using UnityEngine;

public class toggleOrthographic : MonoBehaviour
{
     public void ChangePerspective()
    {
        if(Camera.main.orthographic)
            Camera.main.orthographic = false;
        else
            Camera.main.orthographic = true;
    }
}

