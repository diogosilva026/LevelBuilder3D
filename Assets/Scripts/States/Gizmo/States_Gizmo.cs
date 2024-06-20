using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenState : IGizmo
{
    public GameObject _gizmo;
    public void OnEnterState(GameObject gizmo,  GameObject currentSelection)
    {
        _gizmo = gizmo;
        _gizmo.SetActive(false);
    }
    public void OnUpdateState()
    {}
    public void OnExitState()
    {
        _gizmo.SetActive(true);
    }
}
public class TranState : IGizmo
{
    public GameObject _tran;
    public GameObject _currentSelection;
    public void OnEnterState(GameObject gizmo ,GameObject currentSelection)
    {
        _tran = gizmo.transform.GetChild(2).gameObject;
        _tran.SetActive(true);
        _currentSelection = currentSelection;
    }
    public void OnUpdateState()
    {
        _tran.transform.position = _currentSelection.transform.position;
    }
    public void OnExitState()
    {
        _tran.SetActive(false);
    }
}

public class RotState : IGizmo
{
    public GameObject _rot;
    public GameObject _currentSelection;
    public void OnEnterState(GameObject gizmo, GameObject currentSelection)
    {
        _rot = gizmo.transform.GetChild(0).gameObject;
        _rot.SetActive(true);
        _currentSelection = currentSelection;
        
    }
    public void OnUpdateState()
    {
        _rot.transform.position = _currentSelection.transform.position;
    }
    public void OnExitState()
    {
        _rot.SetActive(false);
    }
}

public class ScaState : IGizmo
{
    public GameObject _sca;
    public GameObject _currentSelection;
    public void OnEnterState(GameObject gizmo, GameObject currentSelection)
    {
        _sca = gizmo.transform.GetChild(1).gameObject;
        _sca.SetActive(true);
        _currentSelection = currentSelection;
    }
    public void OnUpdateState()
    {
        _sca.transform.position = _currentSelection.transform.position;
    }
    public void OnExitState()
    {
        _sca.SetActive(false);
    }
}






