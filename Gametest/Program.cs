// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Vector2 = OpenTK.Mathematics.Vector2;
using System.Threading.Tasks;
using Gametest;

Console.WriteLine("Hello, World!");
GameWindow game = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings() { Size = (800, 900), Title = "hi",Profile = ContextProfile.Compatability});
float[] vertices = {
    0.5f,  0.5f, 0.0f,  // top right
    0.5f, -0.5f, 0.0f,  // bottom right
    -0.5f, -0.5f, 0.0f,  // bottom left
    -0.5f,  0.5f, 0.0f   // top left
};
float[] textcords = {
      0.0f, 0.0f,  // top right
     0.0f, 1.0f,  // bottom right
     1.0f, 1.0f,  // bottom left
      1.0f, 0.0f   // top left
};


uint[] indices = {  // note that we start from 0!
    0, 1, 3,   // first triangle
    1, 2, 3    // second triangle
};
ErrorChecker.InitializeGLDebugCallback();
VAO test = new VAO();

Bufferlayout bufferlayout = new Bufferlayout();
bufferlayout.count = 3;
bufferlayout.normalized = false;
bufferlayout.offset = 0;
bufferlayout.type = VertexAttribType.Float;
bufferlayout.typesize = sizeof(float);
test.LinkAtribute(vertices,bufferlayout);
bufferlayout.count = 2;
test.LinkAtribute(textcords,bufferlayout);

test.LinkElements(indices);




Texture text = new Texture("resources/grass.png");
Shader shader = new Shader("resources/shader.vert", "resources/shader.frag");
shader.Bind();
text.Bind();
shader.setUniform4v("u_Color",2.0f,0.5f,0f,0f);
shader.Unbind();
game.RenderFrame += _ => Update();
game.RenderFrame += _ => game.SwapBuffers();



game.Run();



void Update()
{
    GL.Clear(ClearBufferMask.ColorBufferBit);

    shader.Bind();
    test.Bind();
    GL.DrawElements(PrimitiveType.Triangles,indices.Length,DrawElementsType.UnsignedInt,0);
}