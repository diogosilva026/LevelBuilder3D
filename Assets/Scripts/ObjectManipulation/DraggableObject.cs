using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    
    private void Start(){
        Subject.clean += Implode;
    }

    private void OnDestroy()
    {
        Subject.clean -= Implode;
    }

    public Vector3 screenPoint, offset;

    void OnMouseDown()
    {
        setMouseOffset();
        MouseController.mouse.SetState(new DragState(), gameObject , setMouseOffset());
    }

    //void OnMouseDrag()

     void OnMouseUp() {
         MouseController.mouse.SetState(new FreeState(), null, null);
    }

    void Update()
    {        
      
    }

    Vector3[] setMouseOffset()
    {   
        Vector3[]vecs = new Vector3[2];
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        vecs[0]=screenPoint;
        offset = gameObject.transform.position - 
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        vecs[1]=offset;
        return vecs;
    }

    void Implode(){
        Destroy(gameObject);
    }


}
