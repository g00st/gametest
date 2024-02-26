using OpenTK.Graphics.OpenGL;
using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Net.Mail;
using Gametest.Template;
using OpenTK.Mathematics;
using ImGuiNET;
using OpenTK.ImGui;


namespace Gametest
{
    //ErrorChecker.InitializeGLDebugCallback(); ?
    public class Game1 : GameWindow
    {
        Vector2 possition = new Vector2(0, 0);
        private float scalar = 1000.0f;
        private Radar radar;
        Gamestate gameState = Gamestate.startmenu;
        View Main = new View();
        List<SubView> subViews = new List<SubView>();
        private int Width;
        private int Height;
        public List<DrawObject> objects = new List<DrawObject>();
        private ImGuiController _controller;
        private const int TargetFPS = 60; // Set your target FPS here
        private Random _random = new Random();
        private DateTime _lastFrameTime;

        public float rotation = 0;
       

        public Game1(int width, int height, string title) : base(GameWindowSettings.Default,
            new NativeWindowSettings() { Size = (1920, 1080), Title = "hi", Profile = ContextProfile.Compatability })
        {
           
            Width = width;
            Height = height;
            
            radar = new Radar();
            Texture subViewsurface = new Texture(1000, 1000);
            SubView subView = new SubView(new VBO(subViewsurface));
            subView.addObject(new ColoredRectangle(new Vector2(100, 100), new Vector2(100, 100), Color4.Firebrick));
            subView.addObject(new TexturedRectangle(new Vector2(0), new Vector2(1000, 1000), radar._ScreenTexture2));
            subView.addObject(new TexturedRectangle(new Vector2(0), new Vector2(300, 300), radar.DebugTexture));
            radar.AddObject(new TexturedRectangle(new Vector2(0), new Vector2(10000, 10000), new Texture("resources/Unbenannt.png")));
           
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var h=  new TexturedRectangle(new Vector2(i * 100, j * 100), new Vector2(100, 100), new Texture("resources/radarcrossection2.png"));
                    objects.Add(h);
                    
                    radar.AddObject(h);
                }
            }

            subViews.Add(subView);
            TexturedRectangle t = new TexturedRectangle(subViewsurface);
            t.DrawInfo.Position = new Vector2(1920 / 2 - t.DrawInfo.Size.X / 2, 1080 / 2 - t.DrawInfo.Size.Y / 2);

            Main.addObject(t);
           // Main.addObject(new TexturedRectangle(new Vector2( 0,  0), new Vector2(1000, 1000), new Texture("resources/radarcrossection.png")));

            this.Resize += e => Main.Resize(e.Width, e.Height);
            this.Resize += e => this.resize(e.Width, e.Height);
            this.KeyDown += e => Update(e);
            this.UpdateFrame += _ => rotation++;
        }

        void resize(int width, int height)
        {
            
            if (width != this.Width)
            {
                this.ClientSize = new OpenTK.Mathematics.Vector2i(width, (Width / 16) * 9);
            }
            else if (height != this.Height)
            {
                this.ClientSize = new OpenTK.Mathematics.Vector2i((Height / 9) * 16, height);
            }

            this.Width = width;
            this.Height = height;
        }


        //void update();

        void Update(KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.W:
                    possition.Y -= 10;
                    radar.SetPosition(possition);
                    break;
                case Keys.A:
                    possition.X -= 10;
                    radar.SetPosition(possition);
                    break;
                case Keys.S:
                    possition.Y += 10;
                    radar.SetPosition(possition);
                    break;
                case Keys.D:
                    possition.X += 10;
                    radar.SetPosition(possition);
                    break;


                //case Keys.E: Main.rotation++; break;
                	case Keys.R: scalar = scalar+ 10;
                        radar.SetZoom(scalar);
                        break;
                	case Keys.F: scalar = scalar -10; 
                        radar.SetZoom(scalar);
                        break;
                    
                    case Keys.D1:
                        radar.antnnaSpeed += 10;
                    break;
                    case Keys.D2:
                        radar.antnnaSpeed-= 10;
                    break;                          
                //	case Keys.Q: Main.rotation--; break;
                
                    case Keys.M:
                        radar.setSweep(true);
                        Console.WriteLine("sweep");
                    break;
                    case Keys.N:
                        radar.setSweep(false);
                        Console.WriteLine("sweep false");
                    break;
                    case Keys.D3:
                        radar.setMinAngle(radar.minAngle + 10.0f);
                        Console.WriteLine("min: " + radar.maxAngle + " min: " + radar.minAngle);
                    break;
                    case Keys.D4:
                        radar.setMinAngle(radar.minAngle - 10.0f);
                        Console.WriteLine("min: " + radar.maxAngle + " min: " + radar.minAngle);
                    break;
                    case Keys.D5:
                        radar.setMaxAngle(radar.maxAngle + 10.0f);
                        Console.WriteLine("max: " + radar.maxAngle + " min: " + radar.minAngle);
                    break;
                    case Keys.D6:
                        radar.setMaxAngle(radar.maxAngle - 10.0f);
                        Console.WriteLine("max: " + radar.maxAngle + " min: " + radar.minAngle);
                        break;
                    case Keys.D7:
                        radar.setMaxAngle(radar.maxAngle + 10.0f);
                        radar.setMinAngle(radar.minAngle + 10.0f);
                        Console.WriteLine("max: " + radar.maxAngle + " min: " + radar.minAngle);
                        break;
                    case Keys.D8:
                        radar.setMaxAngle(radar.maxAngle - 10.0f);
                        radar.setMinAngle(radar.minAngle - 10.0f);
                        Console.WriteLine("max: " + radar.maxAngle + " min: " + radar.minAngle);
                        break;
            }

            Console.WriteLine("poss: " + possition + "  size: " +scalar + " rot: " + Main.rotation);
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            var elapsed = DateTime.Now - _lastFrameTime;
            var millisecondsPerFrame = 1000 / TargetFPS;
            if (elapsed.TotalMilliseconds < millisecondsPerFrame)
            {
                var delay = (int)(millisecondsPerFrame - elapsed.TotalMilliseconds);
                System.Threading.Thread.Sleep(delay);
            }

            foreach (var objec in objects)
            {
                objec.DrawInfo.Position+= new Vector2((float)_random.NextDouble()-0.5f, (float)_random.NextDouble()-0.5f);
            }

            _lastFrameTime = DateTime.Now;
            
             
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit );


            

            radar.Draw( args.Time);
            foreach (var view in subViews)
            {
                view.draw();
            }

            Main.draw();

         

            this.SwapBuffers();

        }
    }
}