using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController_ObjSelection
{

    public IObjSelection objSelection;
    public void SetState(IObjSelection state)
    {
        if(objSelection!=null)
        {
            objSelection.OnExitState();
        }
        objSelection=state;

        if(objSelection!=null)
        {
            objSelection.OnEnterState();
        }
    }

    

    public void Update()
    {
        if(objSelection!=null)
        {
            objSelection.OnUpdateState();
        }
    }
}
