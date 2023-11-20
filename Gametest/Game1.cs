using OpenTK.Graphics.OpenGL;
using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Mathematics;


// using var game = new MFC.Game1();
//game.Run();
//hier eigentlich alles von programm rein und programm nur hier ausführen lassen
//die beiden 2zeiler drüber
/*
 * List<DrawInfo> objects = new List<DrawInfo>();
 * List<Renderable> renderobjects = new List<Renderable>();
 */

namespace Gametest
{

	//ErrorChecker.InitializeGLDebugCallback(); ?
	public class Game1 : GameWindow
	{
		Gamestate gameState = Gamestate.startmenu;
		View Main = new View();
		private int Width;
		private int Height;
		public List<DrawInfo> objects = new List<DrawInfo>();
		public float rotation = 0;

		public Game1(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (800, 900), Title = "hi", Profile = ContextProfile.Compatability })
		{
			Width = width;
			Height = height;

			//wie vorlage wavesim
			//hier struktur für game class

			//ab hier alles von programm

			this.VSync = VSyncMode.On;   //in konstruktor this.VSync = VSyncMode.On;
			//Gamestate gameState = new Gamestate();
			//gameState = Gamestate.startmenu;
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

			//re
			uint[] indices = {  // note that we start from 0!
			0, 1, 3,   // first triangle
			1, 2, 3    // second triangle
			};
			//ErrorChecker.InitializeGLDebugCallback();   // nich in konstruktor, danach weiter konstruktor

			Bufferlayout bufferlayout = new Bufferlayout();
			bufferlayout.count = 3;
			bufferlayout.normalized = false;
			bufferlayout.offset = 0;
			//bufferlayout.type = VertexAttribType.Float;
			bufferlayout.typesize = sizeof(float);


			Mesh test2 = new Mesh();
			test2.AddAtribute(bufferlayout, vertices);
			bufferlayout.count = 2;
			test2.AddAtribute(bufferlayout, textcords);
			test2.AddIndecies(indices);

			DrawInfo Body1 = new DrawInfo();
			Body1.mesh = test2;
			Body1.Position = new Vector2(50, 50);
			Body1.Size = new Vector2(100, 100);
			Body1.Rotation = 90.0f;
			//float rotation = 0;
			//View Main = new View();  //update braucht das, maybe globale?
			test lol = new test();
			lol.test2 = Body1;
			Main.addObject(lol);
			//List<DrawInfo> objects = new List<DrawInfo>();
			for (int i = 0; i < 10; i++)
			{
				DrawInfo temp = new DrawInfo();
				temp.mesh = test2;
				temp.Rotation = rotation;
				temp.Size = new Vector2(50, 50);
				temp.Position = new Vector2(50 * i, 50 * i);
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

			this.Resize += e => Main.Resize(e.Width, e.Height);

			//game.RenderFrame += _ => Main.draw();    // zu onrenderframe, override
			//game.RenderFrame += _ => game.SwapBuffers();    // zu onrenderframe, override

			this.KeyDown += e => Update(e);
			this.UpdateFrame += _ => rotation++;
			this.UpdateFrame += _ => urotatio();


		}



		//void update();

		void Update(KeyboardKeyEventArgs e)
		{
			//update gamestate
			/*
			switch(this.gameState)
            {
				case Gamestate.startmenu:
					//draw menu
					//this.backgroundroundColor = Color.Aqua;
					Console.WriteLine("Game starting");
					break;
				case Gamestate.settings:
					//draw settings
					Console.WriteLine("Settings");
					// this.Scene = new List<Drawable>() { this.backButton };
					break;
				case Gamestate.gameisrunning:
					//draw map und rest
					Console.WriteLine("Game running");
                    break;
				case Gamestate.gameover:
					//draw game over
					Console.WriteLine("Game stopped running, back to Menu");
					break;

            }
			*/


			switch (e.Key)
			{
				case Keys.W: Main.vpossition.Y += 100; break;
				case Keys.A: Main.vpossition.X -= 10; break;
				case Keys.S: Main.vpossition.Y -= 10; break;
				case Keys.D: Main.vpossition.X += 10; break;


				case Keys.E: Main.rotation++; break;
				case Keys.R: Main.vsize.X += 10; break;
				case Keys.F: Main.vsize.X -= 10; break;
				case Keys.Q: Main.rotation--; break;



			}
			Console.WriteLine("poss: " + Main.vpossition + "  size: " + Main.vsize + " rot: " + Main.rotation);
			
		}

		protected override void OnRenderFrame(FrameEventArgs args)
		{
			base.OnRenderFrame(args);

			//this.RenderFrame += _ => Main.draw();    // zu onrenderframe, override
			Main.draw(); 
			//this.RenderFrame += _ => this.SwapBuffers();
			this.SwapBuffers();
			 
		}

		//irgendwann camera klasse machen, aber zuerst code aufräumen

		//this.backButton.SetCallback(BackHandler);
		//dann kann man nämlich sagen der backbutton hat macht immer das was im backhandler steht
		//wenn er gedrückt wird zb
		//public void BackHandler(Button self)
		// { this.gameState = GameState.StartMenu;  
		// }
        

	
	void urotatio()
	{
		for (int i = 0; i < objects.Count; i++)
		{
			objects[i].Rotation = rotation;
		}
	}



	



	}


}

