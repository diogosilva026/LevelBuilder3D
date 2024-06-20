using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public Vector3 screenPoint;
    public Vector3 offset;
    public Vector3 currentPosition;
    public Vector3 currentScale;

    // movement gizmos
    public GameObject mMovementGizmo;
    public GameObject GMoveX;
    public GameObject GMoveY;
    public GameObject GMoveZ;

    // rotation gizmos
    public GameObject mRotationGizmo;
    public GameObject GRotateX;
    public GameObject GRotateY;
    public GameObject GRotateZ;

    // scale gizmos
    public GameObject mScaleGizmo;
    public GameObject GScaleX;
    public GameObject GScaleY;
    public GameObject GScaleZ;
}
