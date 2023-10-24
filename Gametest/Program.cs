// See https://aka.ms/new-console-template for more information

using System.Data;
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
game.VSync = VSyncMode.On;
Texture text = new Texture("resources/grass.png");
Shader shader = new Shader("resources/shader.vert", "resources/shader.frag");
float[] vertices = {
    1.0f,  1.0f, 0.0f,  // top right
    1.0f,  0.0f, 0.0f,  // bottom right
     0.0f, 0.0f, 0.0f,  // bottom left
     0.0f,  1f, 0.0f   // top left
};
Console.WriteLine(text.Width);
Console.WriteLine(text.Height);
float[] textcords = {
      16.0f/text.Width, 16.0f/text.Height,  // top right
      16.0f/text.Width,0.0f , // bottom right
     0.0f, 0.0f,  // bottom left
      0.0f, 16.0f/text.Height  // top left
};


uint[] indices = {  // note that we start from 0!
    0, 1, 3,   // first triangle
    1, 2, 3    // second triangle
};
ErrorChecker.InitializeGLDebugCallback();

Bufferlayout bufferlayout = new Bufferlayout();
bufferlayout.count = 3;
bufferlayout.normalized = false;
bufferlayout.offset = 0;
bufferlayout.type = VertexAttribType.Float;
bufferlayout.typesize = sizeof(float);


Mesh test2 = new Mesh();
test2.AddAtribute(bufferlayout,vertices);
bufferlayout.count = 2;
test2.AddAtribute(bufferlayout,textcords);
test2.AddIndecies(indices);

View Main = new View();
Main.addObject(test2);



test2.Shader = shader;
test2.Texture = text;


game.Resize += e => Main.Resize(e.Width, e.Height);
game.RenderFrame += _ => Main.draw();
game.RenderFrame += _ => game.SwapBuffers();
game.KeyDown += e => Update(e);



game.Run();

void Update( KeyboardKeyEventArgs e){
    switch (e.Key)
    {
        case Keys.W: Main.vpossition.Y++; break;
        case Keys.A: Main.vpossition.X--; break;
        case Keys.S: Main.vpossition.Y--; break;
        case Keys.D: Main.vpossition.X++; break;
        
        
        case Keys.E: Main.rotation++; break;
        case Keys.R: Main.vsize.X++; break;
        case Keys.F: Main.vsize.X--; break;
        case Keys.Q:  Main.rotation --; break;
       
       
    } 
    Console.WriteLine("poss: " + Main.vpossition +"  size: " + Main.vsize + " rot: "+ Main.rotation);

}