using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController_Gizmo
{

    GameObject gizmo;

    public StateController_Gizmo(GameObject obj)
    {
        gizmo = obj;
    }

    public IGizmo gizmoState;
    public void SetState(IGizmo state, GameObject? currentSelection)
    {
        if(gizmoState!=null)
        {
            gizmoState.OnExitState();
        }
        gizmoState=state;

        if(gizmoState!=null)
        {
            gizmoState.OnEnterState(gizmo, currentSelection);
        }
    }

    

    public void Update()
    {
        if(gizmoState!=null)
        {
            gizmoState.OnUpdateState();
        }
    }
}
