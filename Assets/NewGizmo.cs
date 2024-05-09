using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewGizmo : MonoBehaviour
{
    public Material highlightMaterial;
    public Material selectionMaterial;
    public GameObject Gizmo;

    private Transform highlight;
    private Material originalMaterialHighlight;
    private Material originalMaterialSelection;
    private RaycastHit raycastHit;
    private RaycastHit raycastHitHandle;
    private Transform selection;

    private int runtimeTransformLayerMask;


    void Start()
    {
        Gizmo.SetActive(false);
    }

    void Update()
    {
        //Para ter os objetos a mudar de cor com o rato por cima
        if (highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                if (highlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                {
                    originalMaterialHighlight = highlight.GetComponent<MeshRenderer>().material;
                    highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
            }
            else
            {
                highlight = null;
            }
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //ApplyLayerToChildren(runtimeTransformGameObj);
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (Physics.Raycast(ray, out raycastHitHandle, Mathf.Infinity, runtimeTransformLayerMask)) //Raycast towards runtime transform handle only
                {
                }
                else if (highlight)
                {
                    if (selection != null)
                    {
                        selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    }
                    selection = raycastHit.transform;
                    if (selection.GetComponent<MeshRenderer>().material != selectionMaterial)
                    {
                        originalMaterialSelection = originalMaterialHighlight;
                        selection.GetComponent<MeshRenderer>().material = selectionMaterial;
                        //runtimeTransformHandle.target = selection;
                        Gizmo.SetActive(true);
                    }
                    highlight = null;
                }
                else
                {
                    if (selection)
                    {
                        selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                        selection = null;

                        Gizmo.SetActive(false);
                    }
                }
            }
            else
            {
                if (selection)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    selection = null;

                    Gizmo.SetActive(false);
                }
            }
        }
    }
}
