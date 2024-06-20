
using UnityEngine;

public class toggleOrthographic : MonoBehaviour
{
    public Camera[] cameras;
     public void ChangePerspective()
    {
        if(Camera.main.orthographic)
        {
            Camera.main.orthographic = false;
            foreach(Camera cam in cameras)
            {
               cam.orthographic = false;
            }
        }
        else{

            Camera.main.orthographic = true;
            foreach(Camera cam in cameras)
            {
               cam.orthographic = true;
            }
        }
    }
}

