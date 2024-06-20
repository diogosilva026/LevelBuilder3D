using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGizmo
{
    void OnEnterState(GameObject gizmo, GameObject currentSelection);
    void OnUpdateState();
    void OnExitState();
}
