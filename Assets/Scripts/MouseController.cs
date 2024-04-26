using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    

    public static Mouse mouse;
    // Start is called before the first frame update
    void Start()
    {
        mouse=new();
        mouse.SetState(new FreeState(), null, null);

    }
    // Update is called once per frame
    void Update()
    {
        mouse.Update();
    }
}
