using Unity.VisualScripting;
using UnityEngine;
public class FreeState : IMouseState
{
    public void OnEnterState(GameObject gameObject , Vector3[] vecs)
    {
        
    }
    public void OnUpdateState()
    {
       
    }
    public void OnExitState()
    {}
}

public class DragState : IMouseState
{
    GameObject go;
    Vector3 offset, curPosition,screenPoint;
    public void OnEnterState(GameObject _gameObject, Vector3[] vecs)
    {
        go = _gameObject;
        screenPoint = vecs[0];
        offset = vecs[1];
    }
    public void OnUpdateState()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        go.transform.position = curPosition;
    }
    public void OnExitState()
    {
        go=null;
    }
}