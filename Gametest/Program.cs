﻿// See https://aka.ms/new-console-template for more information

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
Texture text = new Texture("resources/floorTiles..png");
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
      32.0f/text.Width, 32.0f/text.Height,  // top right
      32.0f/text.Width,0.0f , // bottom right
     0.0f, 0.0f,  // bottom left
      0.0f, 32.0f/text.Height  // top left
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

DrawInfo Body1 = new DrawInfo();
Body1.mesh = test2;
Body1.Position = new Vector2(50, 50);
Body1.Size = new Vector2(100, 100);
Body1.Rotation = 90.0f;
float rotation = 0; 
View Main = new View();
test lol = new test();
lol.test2 = Body1;
Main.addObject(lol);
List<DrawInfo> objects = new List<DrawInfo>();
for (int i = 0; i < 10; i++)
{
    DrawInfo temp = new DrawInfo();
    temp.mesh = test2;
    temp.Rotation =  rotation;
    temp.Size = new Vector2(50, 50);
    temp.Position = new Vector2(50 * i, 50* i);
    //Console.WriteLine(temp.Position);
    //Console.WriteLine(i);
    objects.Add(temp);
    test lol2 = new test();
    lol2.test2 = Body1;
    Main.addObject(lol2);
}







test2.Shader = shader;
test2.Texture = text;


Map dontdie = Loader.LoadMap("resources/Map1/");

Main.addObject(dontdie);

game.Resize += e => Main.Resize(e.Width, e.Height);
game.RenderFrame += _ => Main.draw();
game.RenderFrame += _ => game.SwapBuffers();
game.KeyDown += e => Update(e);
game.UpdateFrame += _ => rotation++;
game.UpdateFrame += _ => urotatio();


game.Run();

void Update( KeyboardKeyEventArgs e){
    switch (e.Key)
    {
        case Keys.W: Main.vpossition.Y+=100; break;
        case Keys.A: Main.vpossition.X-= 10; break;
        case Keys.S: Main.vpossition.Y-= 10; break;
        case Keys.D: Main.vpossition.X+= 10; break;
        
        
        case Keys.E: Main.rotation++; break;
        case Keys.R: Main.vsize.X+= 10; break;
        case Keys.F: Main.vsize.X-=10; break;
        case Keys.Q:  Main.rotation --; break;
       
       
    } 
    Console.WriteLine("poss: " + Main.vpossition +"  size: " + Main.vsize + " rot: "+ Main.rotation);

}

void urotatio(){
    for (int i = 0; i < objects.Count; i++)
    {
        objects[i].Rotation = rotation ;
    }
}