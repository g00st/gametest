namespace Gametest;
using System;
using OpenTK.Graphics.OpenGL;

public static class ErrorChecker
{
    public static void CheckForGLErrors(string context)
    {
        ErrorCode errorCode;
        while ((errorCode = GL.GetError()) != ErrorCode.NoError)
        {
            Console.WriteLine($"OpenGL Error ({context}): {errorCode}");
        }
    }
    public static void GLDebugCallback(DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
    {
        string errorMessage = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(message);
        Console.WriteLine($"OpenGL Debug Message: {source} {type} {id} {severity} - {errorMessage}");
    }
    public static void InitializeGLDebugCallback()
    {
        GL.DebugMessageCallback(GLDebugCallback, IntPtr.Zero);
        GL.Enable(EnableCap.DebugOutput);
    }

}