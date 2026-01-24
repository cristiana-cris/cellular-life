using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;


public class GridOverlay : MonoBehaviour
{
    private Material linemat;

    public bool ShowMain = true;
    public bool ShowSub= true;

    public int GridSizeX;
    public int GridSizeY;

    public float StartX;
    public float StartY;
    public float StartZ;
    public float SmallGrid;
    public float BigGrid;

    public Color mainColor=new Color(1f, 1f, 1f, 1f);
    public Color subColor=new Color(0.5f, 0.5f, 0.5f, 1f);

    
    void CreateLinrmat(){
        //creez materialul
        if(!linemat){
            var shader = Shader.Find("Hidden/Internal-Colored");
            linemat = new Material(shader);
            //disable garbage collector
            linemat.hideFlags = HideFlags.HideAndDontSave;

            //turn on alpha blending
           // linemat.SetInteger("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
          //  linemat.SetInteger("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
           // linemat.SetInteger("_ZWrite", 0);
        }
    }
    void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }

    void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
        DestroyImmediate(linemat);
    }

    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        OnPostRender();
    }

    private void OnPostRender()
    {
         CreateLinrmat();
         linemat.SetPass(0);

        //drawing the lines
        GL.Begin(GL.LINES);

        if(ShowSub){
            GL.Color(subColor);
            for(float y=0.5f; y<=GridSizeY+0.5f; y+=SmallGrid){
                GL.Vertex3(StartX, StartY + y, StartZ);
                GL.Vertex3(StartX + GridSizeX, StartY + y, StartZ);
            }

            for(float x=0.5f; x<=GridSizeX; x+=SmallGrid){
                GL.Vertex3(StartX + x, StartY, StartZ);
                GL.Vertex3(StartX + x, StartY + GridSizeY, StartZ);
            }
        }

        if(ShowMain){
            GL.Color(mainColor);
            for(float y=0.5f; y<=GridSizeY+0.5f; y+=BigGrid){
                GL.Vertex3(StartX, StartY + y, StartZ);
                GL.Vertex3(StartX + GridSizeX, StartY + y, StartZ);
            }

            for(float x=0.5f; x<=GridSizeX; x+=BigGrid){
                GL.Vertex3(StartX + x, StartY, StartZ);
                GL.Vertex3(StartX + x, StartY + GridSizeY, StartZ);
            }
        }

        GL.End();
    }
}
