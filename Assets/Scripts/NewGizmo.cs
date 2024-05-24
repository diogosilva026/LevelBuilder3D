using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewGizmo : MonoBehaviour
{
    public Material highlightMaterial;
    public Material selectionMaterial;
    public GameObject Gizmo;
    public GameObject cuboX;
    public GameObject cuboY;
    public GameObject cuboZ;
    public GameObject myGizmo;

    private Transform highlight;
    private Material originalMaterialHighlight;
    private Material originalMaterialSelection;
    private RaycastHit raycastHit;
    private RaycastHit raycastHitHandle;
    private Transform selection;

    private int runtimeTransformLayerMask;

    void Start()
    {
    }

    void Update()
    {
        //Para ter os objetos a mudar de cor com o rato por cima
        if (highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
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

        //Para ter os objetos a mudar de cor quando estão selecionados pelo cursor
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //ApplyLayerToChildren(runtimeTransformGameObj);
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (Physics.Raycast(ray, out raycastHitHandle, Mathf.Infinity, runtimeTransformLayerMask))
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
                        SpawnGizmo();
                        originalMaterialSelection = originalMaterialHighlight;
                        selection.GetComponent<MeshRenderer>().material = selectionMaterial;
                        //runtimeTransformHandle.target = selection;
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
                }
            }
        }
    }

    void SpawnGizmo()
    {
        //cria uma nova posicao y
        float newYPosition = selection.transform.position.y - 2.1f;

        //cria a posicao final do gizmo
        Vector3 gizmoPosition = new Vector3(selection.transform.position.x, newYPosition, selection.transform.position.z);

        //instancia o gizmo
        myGizmo = Instantiate(Gizmo, gizmoPosition, transform.rotation * Quaternion.Euler(8f, -230f, -8f)) as GameObject;
        myGizmo.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        //transforma o objeto selecionado em filho do Gizmo
        selection.transform.SetParent(myGizmo.transform);

        cuboX = myGizmo.gameObject.transform.GetChild(0).gameObject;
        cuboY = myGizmo.gameObject.transform.GetChild(1).gameObject;
        cuboZ = myGizmo.gameObject.transform.GetChild(2).gameObject;
    }

    public void TranslateSelection()
    {
        Vector3 mousePos = Input.mousePosition;

        if (selection)
        {
            //X
            selection.transform.position = new Vector3(mousePos.x * Time.deltaTime, 0, 0);
        }
        //Y
        selection.transform.position = new Vector3(0, mousePos.y * Time.deltaTime, 0);

        //Z
        selection.transform.position = new Vector3(0, 0, mousePos.x * Time.deltaTime);

    }
}
