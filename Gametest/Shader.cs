﻿using OpenTK.Mathematics;

namespace Gametest;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

public class Shader
{
    private int vertexHandle;
    private int fragmentHandle;
    private int _Handle;
    
    
    public Shader(string vertex, string fragment)
    {
        string vertexData = Loader.LoadVertexShader(vertex);
        string fragementData = Loader.LoadFragmentShader(fragment);
        
         vertexHandle= GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexHandle, vertexData);
        GL.CompileShader(vertexHandle);
        
        fragmentHandle=  GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentHandle, fragementData);
        GL.CompileShader(fragmentHandle);
        

        int res =0;
        GL.GetShader(vertexHandle,ShaderParameter.CompileStatus, out  res);
        if (1 == res) { Console.WriteLine("vertex shader compiled"); } else { Console.WriteLine( " eee1"+ GL.GetProgramInfoLog(vertexHandle)); }
        GL.GetShader(fragmentHandle,ShaderParameter.CompileStatus, out  res);
        if (1 == res) { Console.WriteLine("frag shader compiled"); } else { Console.WriteLine(" eee2"+GL.GetProgramInfoLog(fragmentHandle)); }

        _Handle = GL.CreateProgram();
        GL.AttachShader(_Handle,vertexHandle);
        GL.AttachShader(_Handle,fragmentHandle);
        GL.LinkProgram(_Handle);
        GL.ValidateProgram(_Handle);
    }  
    public void Bind(){ GL.UseProgram(_Handle);}
    public void Unbind (){GL.UseProgram(0);}


    public void setUniform1i(string name,int v1)
    {
        GL.Uniform1( GL.GetUniformLocation(_Handle, name),v1);
    }

    public void setUniformV2f(string name, Vector2 v2)
    {
     
    
        GL.Uniform2( GL.GetUniformLocation(_Handle, name),v2);
    }   
    
    public void setUniformM4(string name,Matrix4 v1)
    {
      
        GL.UniformMatrix4( GL.GetUniformLocation(_Handle, name),false,ref v1);
    }   
    
    public void setUniform4v(string name,float v1,float v2,float v3,float v4)
    {
        GL.Uniform4( GL.GetUniformLocation(_Handle, name),v1,v2,v3,v4);
    }   
    
}