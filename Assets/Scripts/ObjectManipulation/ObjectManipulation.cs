using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectManipulation : MonoBehaviour
{
    public Material highlightMaterial;
    public Material selectionMaterial;

    private Transform highlight;
    private Material originalMaterialHighlight;
    private Material originalMaterialSelection;
    private RaycastHit raycastHit;
    private Transform selection;
    Ray ray;

    public GameObject mainGizmo, rot, sca, tra;

    StateController_Gizmo scGizmo;

    void Start()
    {
        scGizmo = new StateController_Gizmo(mainGizmo);
        scGizmo.SetState(new HiddenState(), null);
    }

    void Update()
    {

        scGizmo.Update();
        //Para ter os objetos a mudar de cor com o rato por cima
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
            highlight = null;
        }
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

            if (Physics.Raycast(ray, out raycastHit))
            {

                if (highlight)
                {
                    //devolve o material original ao asset anteriormente selecionado 
                    if (selection != null)
                    {
                        selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    }
                    //passa a tranform do objeto interceptado para o selection
                if(!selection || selection.GetHashCode() != raycastHit.transform.GetHashCode())
                {
                    selection = raycastHit.transform;
                    Subject.TSelected(selection);
                    Debug.Log(scGizmo);
                    scGizmo.SetState(new TranState(), selection.gameObject);
                }
                

                    if (selection.GetComponent<MeshRenderer>().material != selectionMaterial)
                    {
                        originalMaterialSelection = originalMaterialHighlight;
                        selection.GetComponent<MeshRenderer>().material = selectionMaterial;
                    }
                    highlight = null;
                }
                else
                {
                    if (selection)
                    {
                        selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                        selection = null;
                        
                    }
                }
            }
            else
            {
                if (selection)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    selection = null;
                    scGizmo.SetState(new HiddenState(), null);
                }
            }
        }
        if(selection)
        {
            mainGizmo.transform.position = selection.transform.position;
        }

        if (mainGizmo.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
              scGizmo.SetState(new TranState(), selection.gameObject);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                scGizmo.SetState(new RotState(), selection.gameObject);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                scGizmo.SetState(new ScaState(), selection.gameObject);
            }
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                if (Input.GetKeyDown(KeyCode.G))
                {   
                    Subject.ToggleGrid();
                }
            }

        }
    }// UPDATE BRACKET
}
