using UnityEngine;

public interface IMouseState
{
    void OnEnterState(GameObject gameObject, Vector3[] vecs);
    void OnUpdateState();
    void OnExitState();
}