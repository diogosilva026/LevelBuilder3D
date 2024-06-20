using UnityEditor;
using UnityEngine;

public class LockToGrid : MonoBehaviour
{
    public int tileSize = 1;
    public Vector3 tileOffset = Vector3.zero;
    Transform selected;
    public bool toggleGrid;

    void Start ()
    {
        toggleGrid = false;
        Subject.selected+=SetObj;
        Subject.toggleGrid+=ToggleGrid;
    }
    private void OnDestroy() {
        Subject.selected-=SetObj;
        Subject.toggleGrid-=ToggleGrid;
    }
     void ToggleGrid()
    {
        toggleGrid = !toggleGrid;
        
    }

    void Update()
    {
        if(toggleGrid)
        {
            if(selected != null)
            {
                SnapToGrid(selected);
            }
        }
    }

    void SetObj(Transform t)
    {   
        selected = t;
    }
    void SnapToGrid(Transform t)
    {
            
            Vector3 currentPosition = t.position;
            float snappedX = Mathf.Round(currentPosition.x / tileSize) * tileSize + tileOffset.x;
            float snappedZ = Mathf.Round(currentPosition.z / tileSize) * tileSize + tileOffset.z;
            float snappedY = tileOffset.y; // Preserve the original y-coordinate
            Vector3 snappedPosition = new Vector3(snappedX, snappedY, snappedZ);
            t.position = snappedPosition;
    }
}