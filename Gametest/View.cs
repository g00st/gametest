﻿namespace Gametest;

using System.Runtime.Intrinsics.X86;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;



public class View
{
    private List<Mesh> drawObjects;
    public int  Width, Height;
    public Vector2 vpossition;
    public Vector2 vsize;
    public float rotation;
    
    
   
    

    public void Resize(int width, int height )
    {
        Width = width;
        Height = height;
       
        GL.Viewport(0, 0, Width, Height);
    }
    
    public void addObject(Mesh obj){
        drawObjects.Add(obj);
    }
    public void draw()
    {
        Matrix4 camera =  calcCameraProjection();

        GL.Clear(ClearBufferMask.ColorBufferBit);

        foreach (var obj in drawObjects)
        {
           // Console.WriteLine("hi");
            Matrix4 scaleMatrix = Matrix4.CreateScale(100.0f, 100.0f, 1.0f);
            
          Vector3 cameraRotationAxis = new Vector3(0, 0, 1);
          Matrix4 cameraRotationMatrix = Matrix4.CreateFromAxisAngle(cameraRotationAxis, MathHelper.DegreesToRadians(rotation));
          Matrix4 comb =   (scaleMatrix* Matrix4.CreateTranslation(-vpossition.X,-vpossition.Y,0) * cameraRotationMatrix *Matrix4.CreateTranslation(vpossition.X,vpossition.Y,0) )*camera  ;
            obj.Draw(comb);
        }
    }

    

    private Matrix4 calcCameraProjection()
    {

      
        float zoom = 100;
        float left = vpossition.X - vsize.X / 2.0f;
        float right = vpossition.X + vsize.X / 2.0f;
        float bottom = vpossition.Y -  ((vsize.X/Width)*Height)/ 2.0f;
        float top = vpossition.Y +  ((vsize.X/Width)*Height)/ 2.0f;
        //Console.WriteLine(Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, -1.0f, 1.0f));

        return  Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, -1.0f, 1.0f);

        
    }
    public View()
    {
        vsize = new Vector2(100, 100);
        vpossition = new Vector2(50, 50);
        drawObjects = new List<Mesh>();
        rotation = 0;
    }
    
    
}