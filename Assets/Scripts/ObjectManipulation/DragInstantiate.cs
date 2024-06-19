using UnityEngine;
using UnityEngine.EventSystems;

public class DragInstantiate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject objectToInstantiate;
    private GameObject instantiatedObject;
    private bool isDragging = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (instantiatedObject == null) // Instantiate the object on first click
        {
            instantiatedObject = Instantiate(objectToInstantiate);
            isDragging = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false; // Stop dragging when mouse button is released
        instantiatedObject = null;
    }

    void Update()
    {
        if (isDragging && instantiatedObject != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10; // Set this to be the distance from the camera
            instantiatedObject.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }
}
