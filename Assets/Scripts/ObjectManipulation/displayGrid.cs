using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;



public class displayGrid : MonoBehaviour
{
    // When added to an object, draws colored rays from the
    // transform position.
    public int lineCount = 5;
    public int spacing = 1;

    public int lineLength = 1;

    public float radius = 3.0f;
    static Material lineMaterial;
    bool toggleGrid = false;
    // Start is called before the first frame update
    void Start()
    {
        Subject.toggleGrid+=ToggleGrid;
    }

    private void OnDestroy() {
        Subject.toggleGrid-=ToggleGrid;
    }
    // Update is called once per frame
    void Update()
    {

    }

    void ToggleGrid()
    {
        toggleGrid = !toggleGrid;
        
    }

    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", 1);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    // Will be called after all regular rendering is done
    public void OnRenderObject()
    {
        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);

        if (toggleGrid)
        {

            GL.PushMatrix();
            // Set transformation matrix for drawing to
            // match our transform
            GL.MultMatrix(transform.localToWorldMatrix);

            // Draw lines
            GL.Begin(GL.LINES);
            int pos = 0;
            GL.Color(Color.yellow);
            for (float x = 0; x < lineCount; ++x)
            {
                GL.Vertex3(pos, 0f, -lineLength);
                GL.Vertex3(pos, 0f, lineLength);
                pos += spacing;
            }

            pos = 0;
            for (float y = 0; y < lineCount; ++y)
            {
                GL.Vertex3(-lineLength, 0f, pos);
                GL.Vertex3(lineLength, 0f, pos);
                pos += spacing;
            }
            pos = 0;
            for (float xx = 0; xx < lineCount; ++xx)
            {
                GL.Vertex3(-pos, 0f, -lineLength);
                GL.Vertex3(-pos, 0f, lineLength);
                pos += spacing;
            }

            pos = 0;
            for (float yy = 0; yy < lineCount; ++yy)
            {
                GL.Vertex3(-lineLength, 0f, -pos);
                GL.Vertex3(lineLength, 0f, -pos);
                pos += spacing;
            }
            GL.End();
            GL.PopMatrix();
        }


    }

    /*  private void CreateGrid(Vector3 start, Vector3 stop, float step)
     {

         CreateLineMaterial();
         lineMaterial.SetPass(0);
         GL.Begin(GL.LINES);
         GL.Color(gridColor);


         for (float x = start.x; x <= stop.x; x += step)
         {
             GL.Vertex3(x, 0f, 0f);
             GL.Vertex3(x, stop.y, 0f);
         }


         for (float y = start.y; y <= stop.y; y += step)
         {
             GL.Vertex3(0f, y, 0f);
             GL.Vertex3(stop.x, y, 0f);
         }

         GL.End();
     } */



}
